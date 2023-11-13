namespace _231110
{
    using System.Collections.Generic;
    internal class Program
    {
        private static Character player;
        public static List<Item> inventory;
        public static List<Item> equip;

        // 무기
        public static Weapon oldSword;
        public static Weapon goldSword;
        public static Weapon woodAx;
        public static Weapon silverBow;

        // 방어구
        public static Armor ironArmor;
        public static Armor goldArmor;
        public static Armor silverGloves;

        static void Main(string[] args)
        {           
            GameDataSetting();
            DisplayGameIntro();
        }

        static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
            player = new Character("나재민", "전사", 1, 10, 5, 100, 1500);
            inventory = new List<Item>();
            equip = new List<Item>();

            // 아이템 정보 세팅
            // 1.무기
            oldSword = new Weapon();
            oldSword.itemType = ItemType.Weapon;
            oldSword.Name = "낡은 검";
            oldSword.Description = "쉽게 볼 수 있는 낡은 검입니다.";
            oldSword.Atk = 2;
            inventory.Add(oldSword);

            goldSword = new Weapon();
            goldSword.itemType = ItemType.Weapon;
            goldSword.Name = "황금 검";
            goldSword.Description = "황금으로 만든 비싼 검입니다.";
            goldSword.Atk = 4;
            inventory.Add(goldSword);

            woodAx = new Weapon();
            woodAx.itemType = ItemType.Weapon;
            woodAx.Name = "나무 도끼";
            woodAx.Description = "나무로 만든 도끼입니다.";
            woodAx.Atk = 3;
            inventory.Add(woodAx);

            silverBow = new Weapon();
            silverBow.itemType = ItemType.Weapon;
            silverBow.Name = "은 활";
            silverBow.Description = "은으로 만든 활입니다.";
            silverBow.Atk = 1;
            inventory.Add(silverBow);

            // 2.방어구
            ironArmor = new Armor();
            ironArmor.itemType = ItemType.Armor;
            ironArmor.Name = "무쇠 갑옷";
            ironArmor.Description = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            ironArmor.Def = 5;
            inventory.Add(ironArmor);
            Equip(ironArmor);

            goldArmor = new Armor();
            goldArmor.itemType = ItemType.Armor;
            goldArmor.Name = "황금 갑옷";
            goldArmor.Description = "황금으로 만들어져 튼튼하고 비싼 갑옷입니다.";
            goldArmor.Def = 10;
            goldArmor.Hp = 5;
            inventory.Add(goldArmor);

            silverGloves = new Armor();
            silverGloves.itemType = ItemType.Armor;
            silverGloves.Name = "은 장갑";
            silverGloves.Description = "은로 만든 장갑입니다.";
            silverGloves.Def = 3;
            silverGloves.Hp = 2;
            inventory.Add(silverGloves);
        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    // 작업해보기
                    DisplayInventory();
                    break;
            }
        }

        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk} (+{player.Atk - player.StartAtk})");
            Console.WriteLine($"방어력 :{player.Def} (+{player.Def - player.StartDef})");
            Console.WriteLine($"체력 : {player.Hp} (+{player.Hp - player.StartHp})");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
            }
        }

        static void DisplayInventory()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            foreach (Item it in inventory)
            {
                Console.Write(" - ");

                foreach(Item item in equip)
                {
                    if (item == it)
                    {
                        Console.Write($"[E]");
                        break;
                    }                 
                }

                Console.Write($"{it.Name}");

                if (it.itemType == ItemType.Weapon)
                {
                    Console.Write($" | 공격력 + {((Weapon)it).Atk}"); 
                }
                else
                {
                    Console.Write($" | 방어력 + {((Armor)it).Def}");
                    Console.Write($" | 체 력 + {((Armor)it).Hp}");
                }

                Console.WriteLine($" | {it.Description}");         
            }
            
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:                   
                    DisplayGameIntro();
                    break;

                case 1:
                    // 장착 관리
                    DisplayInventoryManager();
                    break;
            }
        }

        static void DisplayInventoryManager()
        {
            Console.Clear();

            Console.WriteLine("장착관리");
            Console.WriteLine("보유 중인 아이템을 장착 및 해제할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");

            int count = 1;

            foreach (Item it in inventory)
            {
                Console.Write(" - ");
                Console.Write($"{count}. ");

                foreach (Item item in equip)
                {
                    if (item == it)
                    {
                        Console.Write($"[E]");
                        break;
                    }
                }

                Console.Write($"{it.Name}");

                if (it.itemType == ItemType.Weapon)
                {
                    Console.Write($" | 공격력 + {((Weapon)it).Atk}");
                }
                else
                {
                    Console.Write($" | 방어력 + {((Armor)it).Def}");
                    Console.Write($" | 체 력 + {((Armor)it).Hp}");
                }

                Console.WriteLine($" | {it.Description}");

                count++;
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("장착 및 해제할 아이템의 숫자를 입력해주세요.");

            int input = CheckValidInput(0, count - 1);
            if(input == 0)
            {
                DisplayGameIntro();
            }
            else
            {
                foreach (Item item in equip)
                {
                    if (item == inventory[input - 1])
                    {
                        UnEquip(inventory[input - 1]);
                        DisplayInventoryManager();
                    }             
                }
                Equip(inventory[input - 1]);
                DisplayInventoryManager();
            }
        }

        // 장착
        static void Equip(Item item)
        {
            equip.Add(item);

            if (item.itemType == ItemType.Weapon)
            {
                player.Atk += ((Weapon)item).Atk;
            }
            else
            {
                player.Def += ((Armor)item).Def;
                player.Hp += ((Armor)item).Hp;
            }
        }

        // 장착 해제
        static void UnEquip(Item item)
        {
            equip.Remove(item);

            if (item.itemType == ItemType.Weapon)
            {
                player.Atk -= ((Weapon)item).Atk;
            }
            else
            {
                player.Def -= ((Armor)item).Def;
                player.Hp -= ((Armor)item).Hp;
            }
        }

        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }


    public class Character
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int StartAtk { get; set; }
        public int Atk { get; set; }
        public int StartDef { get; set; }
        public int Def { get; set; }

        public int StartHp { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }

        public Character(string name, string job, int level, int startAtk, int startDef, int startHp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            StartAtk = startAtk;
            Atk = startAtk;
            StartDef = startDef;
            Def = startDef;
            StartHp = startHp;
            Hp = startHp;
            Gold = gold;
        }
    }
    
    public enum ItemType
    {
        None,
        Weapon,
        Armor,
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType itemType = ItemType.None;
    }

    public class Weapon : Item
    {
        public int Atk { get; set; }
        
            
    }
    
    public class Armor : Item
    {
        public int Def { get; set; }
        public int Hp { get; set; }
    }
}