using System;
using DungenMonster;
using DungenPlayer;
using DungenFight;

namespace DungenRoom;
public class Room{
    string RoomText;
    Monster RoomMonster;

    public Room(string roomText, Monster roomMonster){
        RoomText = roomText;
        RoomMonster = roomMonster;
    }

    public bool EnterRoom(Player player){ // returns true if player has won the room fight 
        Console.WriteLine($"{RoomText}");
        Console.WriteLine($"The monster in this room has power {RoomMonster.GetPower()}.");
        Console.WriteLine("Starting Fight!");
        Fight fight = new Fight(player, RoomMonster);
        bool hasPlayerWon = fight.StartFight();
        return hasPlayerWon;
    }

    public bool RoomMonsterAlreadyBeaten(){
        if (RoomMonster.GetHp() <= 0){
            return true;
        }
        else {
            return false;
        }
    }
}