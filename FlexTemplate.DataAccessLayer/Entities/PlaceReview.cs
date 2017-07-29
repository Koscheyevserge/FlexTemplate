using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceReview : IComment, IRatingfull
    {
        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Оценка заведения
        /// </summary>
        public int? Rating { get; set; }
        /// <summary>
        /// Id места, к которому оставили отзыв
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// Место, к которому оставили отзыв
        /// </summary>
        public Place Place { get; set; }
        /// <summary>
        /// Модерирован
        /// </summary>
        public bool IsModerated { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int Id { get; set; }
    }
}
