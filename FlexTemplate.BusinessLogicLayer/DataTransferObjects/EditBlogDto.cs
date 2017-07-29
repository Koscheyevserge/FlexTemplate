namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class EditBlogDto
    {
        public int Id { get; set; }
        public string[] Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int[] Categories { get; set; }
    }
}
