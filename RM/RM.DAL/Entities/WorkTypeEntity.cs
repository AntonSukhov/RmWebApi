namespace RM.DAL.Entities
{
    /// <summary>
    /// Сущность вида работ
    /// </summary>
    public class WorkTypeEntity
    {
        #region Свойства

        /// <summary>
        /// ИД вида работ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название вида работ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ИД единицы работ для вида работ
        /// </summary>
        public byte? WorkUnitId { get; set; }

        /// <summary>
        /// Единица работ для вида работ
        /// </summary>
        public WorkUnitEntity WorkUnit { get; set; }

        #endregion
    }
}
