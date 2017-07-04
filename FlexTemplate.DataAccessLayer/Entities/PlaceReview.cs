namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceReview : BaseAuthorfullEntity
    {
        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Оценка заведения
        /// </summary>
        public int Star { get; set; }
        /// <summary>
        /// Id места, к которому оставили отзыв
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// Место, к которому оставили отзыв
        /// </summary>
        public Place Place { get; set; }
        public bool IsModerated { get; set; }
    }
}
