using TODOLIST.Data.Abstract;
using TODOLIST.Model.Entities;

namespace TODOLIST.Data.Repositories
{
    public class StickerRepository : EntityBaseRepository<Sticker>, IStickerRepository
    {
        public StickerRepository(TodolistContext context) : base(context)
        {
            
        }
    }
}