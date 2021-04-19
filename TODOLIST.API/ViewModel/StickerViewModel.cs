namespace TODOLIST.API.ViewModel
{
    public class StickerViewModel
    {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Color { get; set; }
        public string Content { get; set; }

        public string Url { get; set; } = null;
    }
}