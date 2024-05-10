using Google.Apis.Walletobjects.v1.Data;
using Google;
using Google.Apis.Walletobjects.v1;

namespace adminWPF
{
    internal class ReplasibleMessages
    {
        private readonly WalletobjectsService _service;

        string _messageBody;

        //Создание конструктора
        public ReplasibleMessages(WalletobjectsService service) {
            _service = service;
        }

        //сообщение с таким же заголовком будет заменено
        public void ReplaceMessageInLoyaltyObject(string objectId, Message newMessage, string messageHeaderToReplace = null) {
            try
            {
                // Получение объекта карты по ID
                LoyaltyObject loyaltyObject = _service.Loyaltyobject.Get(objectId).Execute();

                // Если сообщений нет, создаем новый список
                if (loyaltyObject.Messages == null)
                {
                    loyaltyObject.Messages = new List<Message>();
                }
                else if (!string.IsNullOrEmpty(messageHeaderToReplace))
                {
                    // Находим сообщение с таким же заголовком, если оно существует
                    var existingMessage = loyaltyObject.Messages.FirstOrDefault(m => m.Header == messageHeaderToReplace);
                    if (existingMessage != null)
                    {
                        _messageBody = existingMessage.Body;
                        // Удаляем существующее сообщение
                        loyaltyObject.Messages.Remove(existingMessage);
                    }
                }

                // Добавление нового сообщения в объект карты
                loyaltyObject.Messages.Add(newMessage);

                // Обновление объекта карты
                _service.Loyaltyobject.Patch(loyaltyObject, objectId).Execute();
            }
            catch (GoogleApiException ex)
            {
                //Вывод сообщения об ошибке
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public string GetMessageBody(string objectId, string messageHeader) {
            try
            {
                // Получение объекта карты по ID
                LoyaltyObject loyaltyObject = _service.Loyaltyobject.Get(objectId).Execute();

                // Поиск сообщения по заголовку
                var message = loyaltyObject.Messages?.FirstOrDefault(m => m.Header == messageHeader);
                return message?.Body; // Возвращает тело найденного сообщения или null, если сообщение не найдено
            }
            catch (GoogleApiException ex)
            {
                // Вывод сообщения об ошибке
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // В случае ошибки возвращаем null
            }
        }
    }
}
