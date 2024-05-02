using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;

public class LoyaltyCardManager
{
    private WalletobjectsService _walletService;

    public LoyaltyCardManager(WalletobjectsService walletService) {
        _walletService = walletService;
    }

    public void UpdateLoyaltyPoints(string loyaltyCardId, int additionalPoints) {
        LoyaltyObject loyaltyObject = _walletService.Loyaltyobject.Get(loyaltyCardId).Execute();

        int currentPoints = loyaltyObject.LoyaltyPoints.Balance.Int__ ?? 0;


        LoyaltyObject patchObject = new LoyaltyObject();
        patchObject.LoyaltyPoints = new LoyaltyPoints();
        patchObject.LoyaltyPoints.Balance = new LoyaltyPointsBalance();
        patchObject.LoyaltyPoints.Balance.Int__ = currentPoints + additionalPoints;

        _walletService.Loyaltyobject.Patch(patchObject, loyaltyCardId).Execute();
    }
}
