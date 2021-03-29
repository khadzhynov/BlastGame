using BlastGame.Core.Views;

namespace BlastGame.Core.Controllers
{
    public interface IFieldItemEventListener
    {
        void StoneClicked(StoneView stone);
        void PowerUpClicked(PowerUpView powerUp);
        void PowerUpsTouched(PowerUpView first, PowerUpView second);
    }
}