using GameStore.ViewModels;

namespace GameStore.Services;

public interface IGameService{

    Task Create (CreateGameViewModel model);
}