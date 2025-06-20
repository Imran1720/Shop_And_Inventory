public interface ICoinManager
{
    void IncrementCoins(int amount);
    void DecrementCoins(int amount);
    int GetTotalCoins();
    void SetCoinUI();
}
