using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Дата создания записи (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата последнего обновления записи (UTC)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// ID пользователя, который создал запись (если есть авторизация)
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// ID пользователя, который обновил запись
        /// </summary>
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Флаг мягкого удаления (soft delete)
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Дата удаления (при soft delete)
        /// </summary>
        public DateTime? DeletedAt { get; set; }
    }
}
