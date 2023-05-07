namespace RM.DAL.Entities
{
    /// <summary>
    /// Сущность единицы работ
    /// </summary>
    public class WorkUnitEntity
    {
        #region Свойства

        /// <summary>
        /// ИД единицы работ
        /// </summary>
        public byte Id { get; set; }

        /// <summary>
        /// Название единицы работ
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
