# 스파르타 던전(Text 게임) 만들기 프로젝트

## 프로젝트 소개
C# 스파르타 던전(Text 게임) 만들기 프로젝트.

##  기술 스택
![C#](https://img.shields.io/badge/-C%23-%7ED321?logo=Csharp&style=flat)

## 구현 기능
* 게임시작 화면
* 상태보기
* 인벤토리
    * 인벤토리 List로 관리
    * 아이템 정보 클래스로 구현
    * 장착관리
      
## 구현 기능 세부 설명

### 게임시작 화면
* 게임 시작시 간단한 소개 말과 마을에서 할 수 있는 행동 출력
* 상태보기와 인벤토리 이동
    
### 상태보기
* 캐릭터의 정보 (레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold) 표시
* 장착된 아이템에 따라 수치 변경
 
### 인벤토리, 장착 아이템 List로 관리
```
public List<Item> inventory = new List<Item>();
public List<Item> equip = new List<Item>();
```

### 아이템 정보 클래스로 구현
 ``` 
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
  ```

### 인벤토리 아이템 출력
```
foreach (Item it in inventory)
{
    Console.Write(" - ");
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
```

### 장착 List 순회하여 장착 확인
```
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
}
```

### 장착 아이템 캐릭터 정보 반영
```
void Equip(Item item)
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

void UnEquip(Item item)
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
```

## 게임 화면

### 게임시작
<img src="https://github.com/JaeMinNa/SpartanTextGame/assets/149379194/1bd614f2-c172-4e16-b5b9-dd5909f41734" width="1000">


### 인벤토리
<img src="https://github.com/JaeMinNa/SpartanTextGame/assets/149379194/7ce33fd0-5a87-4e0f-a8a7-1af26c6133c0" width="1000">


### 장비 장착
<img src="https://github.com/JaeMinNa/SpartanTextGame/assets/149379194/a40faebb-868e-46a8-9407-e08144e57a81" width="1000">


### 상태보기
<img src="https://github.com/JaeMinNa/SpartanTextGame/assets/149379194/ad8b7a68-ae7e-407f-bcf9-5d89a421cc8b" width="1000">


## 프로젝트 시 일어난 문제와 해결  

### Weapon, Armor 클래스 변수 불러오기
```
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
```
가장 어려웠던 점은 List로 관리하는 Item의 정보를 불러올 때, 자식 클래스인 Weapon과 Armor의 변수를 불러오는 것이었다.

자식 클래스에서는 부모 클래스의 변수를 그냥 불러올 수 있지만, 반대인 경우는 불러올 수 없었다.

한참을 고민하고 구글링을 통해 클래스의 형변환을 통해서 불러올 수 있다는 것을 알게되었다. 

### 장착 아이템 확인
모든 Item을 하나의 리스트로 관리했는데, 아이템의 장착을 어떻게 확인하고 캐릭터 정보에 어떻게 반영할지 쉽게 떠오르지 않았다.

Item을 관리하는 리스트를 하나 더 만들어서 장착 아이템을 따로 관리했다. 그래서 그 List에 있는 아이템만 캐릭터 능력치에 반영하도록 했다.


## 프로젝트 소감
Text RPG 게임은 예전에 한번 만들어본 경험이 있었다. 그래서 이번 프로젝트는 Item을 관리하는 부분에 가장 신경을 많이 썼다.

List를 활용해서 전체 Item을 관리하여 인벤토리를 구현하고 싶었고, 어렵게 구현을 성공했다.

이 부분에서 막혀서 몇시간을 고민하고 구글링을 했다. 이러한 노력으로 앞으로는 절대 까먹지 않을 것 같다. 

프로젝트를 통해, 알고있다고 생각했던 부분도 쉽게 구현하기 어려웠고, 어떠한 내용이든 실습을하고 내 것으로 만드는 것이 더욱 중요하다고 느꼈다.

