using System.Collections;
using UnityEngine;

public class MatrixGrid
{
    private Singleton _singletonManager;
    public static MineScript[,] mineBlocks;
    
    void Awake() {
        _singletonManager = Singleton.Instance;
    } //-- Awake end

    public static void ShowAllMines() {
        foreach (MineScript ms in mineBlocks) {
            ms.ShowMines();
        }
    } //-- ShowAllMines end

    public static bool MineAtCoordinates(int x, int y) {
        var manager = Singleton.Instance;
        if (x >= 0 && y >= 0 && x < manager.rows && y < manager.columns) {
            if (mineBlocks[x, y].isMine)
                return true;
        }
        return false;
    } //-- MineAtCoordinates end

    public static int NearbyMines(int x, int y) {
        int count = 0;

        if (MineAtCoordinates(x, y + 1))
            count++;
        if (MineAtCoordinates(x, y - 1))
            count++;
        if (MineAtCoordinates(x + 1, y))
            count++;
        if (MineAtCoordinates(x - 1, y))
            count++;

        if (MineAtCoordinates(x + 1, y + 1))
            count++;
        if (MineAtCoordinates(x - 1, y + 1))
            count++;
        if (MineAtCoordinates(x + 1, y - 1))
            count++;
        if (MineAtCoordinates(x - 1, y - 1))
            count++;

        return count;
    } //-- NearbyMines end

    public static void CheckMines(int x, int y, bool[,] visited) {
        var manager = Singleton.Instance;
        if (x >=  0 && y >= 0 && x < manager.rows && y < manager.columns) {
            if(visited[x, y]) return;

            mineBlocks[x, y].ShowMines();
            mineBlocks[x, y].ShowNearbyMinesCount(NearbyMines(x, y));

            if (NearbyMines(x, y) > 0)
                return;

            visited[x, y] = true;
            CheckMines(x + 1, y, visited);
            CheckMines(x - 1, y, visited);
            CheckMines(x, y + 1, visited);
            CheckMines(x, y - 1, visited);
            CheckMines(x + 1, y + 1, visited);
            CheckMines(x - 1, y + 1, visited);
            CheckMines(x + 1, y - 1, visited);
            CheckMines(x - 1, y - 1, visited);
        }
    } //-- CheckMines end

    
    public static bool CheckBlockStatus() {
        foreach(MineScript block in mineBlocks) {
            if(block.IsClicked() && !block.isMine) {
                return false;
            }
        }
        return true;
    } //-- CheckBlockStatus end
}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/