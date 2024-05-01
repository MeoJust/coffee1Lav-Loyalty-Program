using Google;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;

//Проверка карт
public class LoyaltyCardService
{
    private readonly WalletobjectsService _service;

    public LoyaltyCardService(WalletobjectsService service) {
        _service = service;
    }

    //Проверка наличия и статуса конкретного обьекта карты
    public LoyaltyObject GetLoyaltyObject(string objectId) {
        try
        {
            // Получение объекта карты лояльности по ID
            LoyaltyObject loyaltyObject = _service.Loyaltyobject.Get(objectId).Execute();
            // Проверка статуса обьекта
            Console.WriteLine($"Object State: {loyaltyObject.State}");
            return loyaltyObject;
        }
        catch (GoogleApiException ex)
        {
            //Вывод сообщения об ошибке
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    //Получение списка всех активных обьектов карты
    public IList<LoyaltyObject> GetAllLoyaltyObjects(string classId) {
        try
        {
            // Создание запроса на получение списка объектов определенного класса карты
            LoyaltyobjectResource.ListRequest request = _service.Loyaltyobject.List();
            request.ClassId = classId; 
            LoyaltyObjectListResponse response = request.Execute();
            IList<LoyaltyObject> loyaltyObjects = response.Resources;
            foreach (var lo in loyaltyObjects)
            {
                Console.WriteLine(lo.Id);
            }
            return loyaltyObjects;
        }
        catch (GoogleApiException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}
