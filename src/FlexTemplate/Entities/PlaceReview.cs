using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class PlaceReview : BaseEntity
    {
        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Id пользователя, который оставил отзыв
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Пользователь оставивший отзыв
        /// </summary>
        public User User { get; set; }
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
    }
}
