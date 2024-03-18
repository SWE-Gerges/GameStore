using GameStore.Models;
using GameStore.ViewModels;

namespace GameStore.Services;

public interface IGameService{

    Task Create (CreateGameViewModel model);

    IEnumerable<Game> GetAll();

    Game? GetById(int id);

    void Edit(int id, Game updatedGame);
}