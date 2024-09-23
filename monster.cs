using System;
using DungenPlayer;

namespace DungenMonster;
public class Monster{
    private int Hp = 100;
    private int Power;

    public Monster(int power){
        Power = power;
    }

    public bool LostFight(){
        if (Hp <= 0){
            return true;
        }
        else {
            return false;
        }
    }

    public void GotHit(Player player){
        int damage = (int) (((float) player.GetPower() / Power)*25);
        Hp -= damage;
        Hp = (Hp > 0) ? Hp : 0; // returns max between Hp and 0
        Console.WriteLine($"Successful hit! Monster's Hp decreased to {Hp}!");
    }

    public int GetPower(){
        return Power;
    }

    public int GetHp(){
        return Hp;
    }
}