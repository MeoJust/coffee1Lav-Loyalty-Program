using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;

namespace adminWPF
{
    internal class BonusPointsManager
    {
        private WalletobjectsService _walletService;

        public BonusPointsManager(WalletobjectsService walletService) {
            _walletService = walletService;
        }

        //Логика изменения балланса бонусных баллов
        public void UpdateLoyaltyPoints(string loyaltyCardId, int additionalPoints) {
            LoyaltyObject loyaltyObject = _walletService.Loyaltyobject.Get(loyaltyCardId).Execute();
            //Получение текущего балланаса
            int currentPoints = loyaltyObject.LoyaltyPoints.Balance.Int__ ?? 0;


            LoyaltyObject patchObject = new LoyaltyObject();
            patchObject.LoyaltyPoints = new LoyaltyPoints();
            patchObject.LoyaltyPoints.Balance = new LoyaltyPointsBalance();
            //Изменение баллов
            patchObject.LoyaltyPoints.Balance.Int__ = currentPoints + additionalPoints;

            //Баллы не могт быть меньше нуля
            if (patchObject.LoyaltyPoints.Balance.Int__ < 0) {
                patchObject.LoyaltyPoints.Balance.Int__ = 0;
            }

            _walletService.Loyaltyobject.Patch(patchObject, loyaltyCardId).Execute();
        }
    }
}
