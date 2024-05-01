using Google;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;

namespace adminWPF
{
    //Логика отправки уведомлений
    public class NotifiSender
    {
        private readonly WalletobjectsService _service;

        //Создание конструктора
        public NotifiSender(WalletobjectsService service) {
            _service = service;
        }

        //Отправка уведомления
        public void AddMessageToLoyaltyObject(string objectId, Message message) {
            try
            {
                // Получение объекта карты по ID
                LoyaltyObject loyaltyObject = _service.Loyaltyobject.Get(objectId).Execute();

                // Создание сообщения
                if (loyaltyObject.Messages == null)
                {
                    loyaltyObject.Messages = new List<Message>();
                }
                loyaltyObject.Messages.Add(message);

                // Добавление сообщения в объект карты
                _service.Loyaltyobject.Patch(loyaltyObject, objectId).Execute();
            }
            catch (GoogleApiException ex)
            {
                //Вывод сообщения об ошибке
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
