using System;
using DungenPlayer;
using DungenMonster;

namespace DungenFight;
public class Fight{

    Player FightPlayer;
    Monster FightMonster;
    private bool IsFightOver = false;
    private bool HasPlayerWon = false;
    Random random = new Random();
    
    public Fight(Player player, Monster monster){
        FightPlayer = player;
        FightMonster = monster;
    }

    public bool StartFight(){ // returns true if player has won
        while(IsFightOver is false){
            PlayerTurn();
            if (FightMonster.LostFight()){
                IsFightOver = true;
                HasPlayerWon = true;
            }
            MonsterTurn();
            if (FightPlayer.LostFight()){
                IsFightOver = true;
                HasPlayerWon = false;
            }
        }

        if (HasPlayerWon){
            Console.WriteLine("Player won the fight!");
            FightPlayer.WonFightActions();
        }
        else {
            FightPlayer.LostFightActions();
            if (FightMonster.LostFight()){ 
                Console.WriteLine("Both player and moster lost all their hp!");
                FightPlayer.WonFightActions();
            }
            else {
                 Console.WriteLine("Player lost the fight:(");
            }
        }

        return HasPlayerWon;
    }

    private void PlayerTurn(){
        int rnd = random.Next(1,3); // 1 or 2
        if (rnd == 1){ // Hit has 50% to be successful 
            FightMonster.GotHit(FightPlayer);
        }
        else {
            Console.WriteLine("Player's hit was unsuccessful! Monsters Hp remained the same:(");
        }
    }

     private void MonsterTurn(){
        int rnd = random.Next(1,3); // 1 or 2
        if (rnd == 1){ // Hit has 50% to be successful 
            FightPlayer.GotHit(FightMonster);
        }
        else {
            Console.WriteLine("Monster's hit was unsuccessful! Player's Hp remained the same:)");
        }
    }
}