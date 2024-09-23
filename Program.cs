using System;
using DungenPlayer;
using DungenMonster;
using DungenRoom;
using System.Threading;

namespace DungenGame;
public class Game{
    static int MAX_TRIES = 10;
    static int NUMBER_OF_ROOMS = 10;
    static List<Room> Rooms = new List<Room>();
    static List<List<Room>> GridRooms = new List<List<Room>>();
    static bool LastRoom;
    static bool gameOver = false;

    public static void Main(){
        Console.WriteLine("Game starts!");
        Console.WriteLine("To play a linear game, type 'L', for a grid game, type 'G' & enter:");
        string? gameType = Console.ReadLine();
        if (gameType == "L"){
            PlayLinearGame();
        }
        else if (gameType == "G"){
            PlayGridGame();
        }
        else {
            Console.WriteLine("Invalid choice.");
        }

    }

    static void PlayLinearGame(){
        InitializeLinearGame();
        int RoomNum = 0;
        Player player = new Player();
        while (gameOver is false){
            foreach(Room room in Rooms){
                RoomNum++;
                if (RoomNum == NUMBER_OF_ROOMS){
                    LastRoom = true;
                }
                else {
                    LastRoom = false;
                }
                Thread.Sleep(1000); // for better interactivity
                bool hasPlayerWon = room.EnterRoom(player);
                if (GameOver(player, hasPlayerWon)){
                    gameOver = true;
                    break;
                }
                if (hasPlayerWon is false){
                    Console.WriteLine("Starting dungen from beginning.");
                    RoomNum = 0;
                    InitializeLinearGame(); // initializing rooms - restores monster's hp etc
                    break; // starts dungen from the beginning
                }
            } 
        }
    }

    static void PlayGridGame(){
        InitializeGridGame();
        Player player = new Player();
        List<int> currRoom = [0,0];
        int i = 0;
        int j = 0;
        while (gameOver is false){
            if (i == NUMBER_OF_ROOMS-1 && j ==  NUMBER_OF_ROOMS-1){
                LastRoom = true;
            }
            else {
                LastRoom = false;
            }
            Thread.Sleep(1000); // for better interactivity
            Room room = GridRooms[i][j];
            if (room.RoomMonsterAlreadyBeaten()){
                Console.WriteLine("You already beaten the monster in this room!");
            }
            else {
                bool hasPlayerWon = room.EnterRoom(player);
                if (GameOver(player, hasPlayerWon)){
                    gameOver = true;
                    continue;
                }
                if (hasPlayerWon is false){
                    Console.WriteLine("Starting dungen from beginning.");
                    i = 0;
                    j = 0; // starts dungen from the beginning
                    InitializeGridGame();
                    continue;
                }
            }
                // if got here, hasPlayerWon is true -> player won the room fight, or the monster in the current room was already beaten
                Console.WriteLine("Which room would you like to move to?");
                PrintOptions(i, j);
                Console.WriteLine("Your choice (write in format: i,j):");
                string? choice = Console.ReadLine();
                string[] i_and_j = choice.Split(','); // lacks input check, sorry
                i = Int32.Parse(i_and_j[0]);
                j = Int32.Parse(i_and_j[1]);
        }
    }

    static void PrintOptions(int i, int j){
        Console.WriteLine("Options:");
        if (i > 0) {
            Console.WriteLine($"({i-1},{j})");
        }
        if (j > 0) {
            Console.WriteLine($"({i},{j-1})");
        }
        if (i < NUMBER_OF_ROOMS-1){
            Console.WriteLine($"({i+1},{j})");
        } 
        if (j < NUMBER_OF_ROOMS-1){
            Console.WriteLine($"({i},{j+1})");
        }
    }

    static bool GameOver(Player player, bool hasPlayerWon){
        if (player.GetTryNumber() == MAX_TRIES){
            Console.WriteLine($"Player lost all {MAX_TRIES} lives! Game Over!!");
            return true;
        }
        else if (LastRoom is true && hasPlayerWon){
            Console.WriteLine("PLAYER WON!");
            return true;
        }
        else {
            return false;
        }
    }

    static void InitializeLinearGame(){
        Rooms = new List<Room>();
        for(int i=0; i<NUMBER_OF_ROOMS; i++){
            Monster RoomMonster = new Monster(1+i*4); // each level the monster gets stronger by 4.
            Room room = new Room($"Welcome to room number {i+1}!", RoomMonster);
            Rooms.Add(room);
        }
    }

    static void InitializeGridGame(){
        GridRooms = new List<List<Room>>();
        for(int i=0; i<NUMBER_OF_ROOMS; i++){
            GridRooms.Add(new List<Room>());
            for(int j=0; j<NUMBER_OF_ROOMS; j++){
                Monster RoomMonster = new Monster(1 + j*2 + i*2); // monsters get stronger when going right and down
                Room room = new Room($"Welcome to room number {i},{j}!", RoomMonster);
                GridRooms[i].Add(room);
            }
        }
    }
}