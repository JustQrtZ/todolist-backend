using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TODOLIST.API.ViewModel;
using TODOLIST.Data.Abstract;

namespace TODOLIST.API.Sticer
{
    public class StickerHub : Hub
    {
        private readonly IStickerRepository _context;

        public StickerHub(IStickerRepository context)
        {
            _context = context;
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("JoinGroup", groupName);
            var allStickers = _context.GetAll();
            await Clients.Client(Context.ConnectionId).SendAsync("allStickers", allStickers);
        }
        
        
        
        public async Task DeleteSticker(DeleteStickerViewModel deletemodel)
        {
            _context.DeleteWhere(sticker => deletemodel.Id == sticker.Id);
            _context.Commit();
            await Clients.Group(deletemodel.Group).SendAsync("DeleteSticker",_context.GetAll());
        }
        
        public async Task AddSticker(StickerWithGroup stickerModel)
        {
            if (stickerModel.Model.Content == null && stickerModel.Model.Url == null)
            {
                return;
            }

            string id = Guid.NewGuid().ToString();
            var sticker = new Model.Entities.Sticker
            {
                Id = id,
                X = stickerModel.Model.X,
                Y = stickerModel.Model.Y,
                Width = stickerModel.Model.Width,
                Height = stickerModel.Model.Height,
                Color = stickerModel.Model.Color,
                Content = stickerModel.Model.Content,
                Url = stickerModel.Model.Url
            };
            
            _context.Add(sticker);
            _context.Commit();
            await Clients.Group(stickerModel.Group).SendAsync("AddStickerSuccess", _context.GetAll());
        }
        
        public async Task EditSticker(StickerWithGroup stickerModel)
        {
            if (stickerModel.Model.Content == null && stickerModel.Model.Url == null)
            {
                return;
            }
            var sticker = _context.GetSingle(x => stickerModel.Model.Id == x.Id);
            
            
            sticker.X = stickerModel.Model.X;
            sticker.Y = stickerModel.Model.Y;
            sticker.Width = stickerModel.Model.Width;
            sticker.Height = stickerModel.Model.Height;
            sticker.Color = stickerModel.Model.Color;
            sticker.Content = stickerModel.Model.Content;
            sticker.Url = stickerModel.Model.Url;

            _context.Update(sticker);
            _context.Commit();
            await Clients.Group(stickerModel.Group).SendAsync("EditStickerSuccess", _context.GetAll());
        }
    }
}