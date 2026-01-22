namespace PCConfigurator.Models
{
    /// <summary>
    /// Модель для отображения информации об ошибках
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Флаг, показывающий, нужно ли отображать идентификатор запроса
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}