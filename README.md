# 스파르타 던전(Text 게임) 만들기 프로젝트

## 프로젝트 소개
C# 스파르타 던전(Text 게임) 만들기 프로젝트입니다.

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



## 프로젝트 시 일어난 문제와 해결  

### 프로젝트

__문제__:  
 
데이터가 지워졌다는 경고문이 사라지지 않았음
구현하기로 한 내용 : 데이터를 삭제 버튼을 누르면 데이터가 지워졌다는 경고가 뜨고 1.5초 후에 알아서 사라지도록 함.
문제가 발생한 상황 : 맨 처음 게임을 실행한 상태에서는 이상 없이 창이 닫혔지만 어떻게든 게임이 끝나거나 게임씬에서 스타트씬으로 돌아온 경우에는 (데이터 유무 관계 없이) 창이 닫히지 않음.
문제 해결을 위해 노력한 것 : 처음 의심은 데이터 여부가 영향을 준다고 생각했음. 해결을 위해 Coroutine 함수 등을 이용해보았으나, 동일한 증상이 반복되어 튜터님과 함께 해당 기능에 대하여 이야기를 나눔  

__결과__:  

 “씬 이동“에 문제가 있는 것 같다는 결론에 도달.
해결방안 : 우선 Invoke 함수가 실행되지 않음을 확인. 그러다 게임씬 시간을 확인해보니 0에 고정이 되어 있었음. timeScale에 대한 이야기 후 게임씬에서 스타트씬을 다시 로드할 때 timeScale을 0으로 만들도록 설정이 되어 있음을 확인. 시간이 고정되면 Invoke 함수가 참고할 시간이 없으므로 작동 불가. 홈으로 돌아가는 버튼에 timeScale을 1로 재설정하는 기능을 추가하여 시간이 다시 흐르도록 함. > 해결!  

### GitHub  

__문제__:  

Branch를 옮길 때 Scene 내부에서 작업한 것끼리 충돌 발생  

__증상__:  

GitHub 내에서 어떤 Scene 살릴 것을 몰라 둘다 사라짐 >> 작업내용이 날아가고, GitHub가 놓아주지 않음… (이동X)
다른 멤버가 대신 branch 삭제하고 GitHub 재접. (카드 뒤집기 모션 X)
브랜치 옮길 때, Bring change  사용 X (Stash로 고정)

## 프로젝트 소감

<br/>
