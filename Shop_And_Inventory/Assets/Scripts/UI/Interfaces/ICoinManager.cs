public interface ICoinManager
{
    void SetCoinUI();
    void IncrementCoins(int amount);
    void DecrementCoins(int amount);
    int GetTotalCoins();
}
