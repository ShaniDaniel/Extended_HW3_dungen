using System.ComponentModel.DataAnnotations;
using DungenMonster;

namespace DungenPlayer;

public class Player{
    private int Level = 1;
    private int XP = 0;
    private int Hp = 100;
    private int Power = 1;
    private int TryNumber = 1;

    public Player(){

    }

    public bool LostFight(){
        if (Hp <= 0){
            return true;
        }
        else {
            return false;
        }
    }

     public void GotHit(Monster monster){
        int damage =(int) (((float) monster.GetPower() / Power)*25);
        Hp -= damage;
        Hp = (Hp > 0) ? Hp : 0; // returns max between Hp and 0
        Console.WriteLine($"Ouch! Player's Hp decreased to {Hp}");
    }

    public void LostFightActions(){
        TryNumber++;
        Hp = 100 + (Level-1)*10; // Hp restored 
    }

    public void WonFightActions(){
        XP += Power*10; // XP gained each won fight
        while (XP > 10 + (Level-1)*30 + (Power/2)*50){ // The amount of XP needed to level up
            Console.WriteLine("Leveled up!");
            Level +=1;
            Power += 2; // Power increased by 2 each level
        }
        Hp = 100 + (Level-1)*10; // Hp restored & increased by 10 each level
        

        Console.WriteLine($"Player is level {Level}! Power is {Power}, Hp are {Hp}");
    }

    public int GetTryNumber(){
        return TryNumber;
    }

    public int GetPower(){
        return Power;
    }
}