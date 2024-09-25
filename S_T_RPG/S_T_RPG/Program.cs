using System.Collections.Generic;

namespace S_T_RPG
{
    //I캐릭터 Name HP AttackPower IsDead 공격받음(상대방객체의Attack);
    //C워리어:I캐릭터
    //C몬스터:I캐릭터
    //C고블린:몬스터
    //C드래곤:몬스터
    //I아이템 Name 사용();
    //C체력포션:I아이템
    //C공격포션:I아이템
    //C스테이지 인자3개 받아야함(캐릭터,몬스터,전리품) 생성자(인자3) start()둘중 하나가 죽을때까지 반복
    //메인 객체생성 gameManager역할 스테이지(캐릭터,몬스터,전리품)
    //각각의 초기화는 클래스에서 변동사항은 인자에 따라 클래스내에서 초기화
    //게임종료 if return if를 호출하는 함수를 벗어남;

    interface ICharacter
    { 
        //상속 받는 클래스는 무조건 인터페이스의 필드,함수를 구현해야한다(필수구현부)
        public string Name { get; }
        public int HP { get; set; }
        public int Power { get; set; }
        public int Attack { get; }
        public bool IsDead { get; }
        public void TakeDamage(int damage)
        {
            
        }

    }

    class Warrior : ICharacter
    {
        public string Name { get; }
        public int HP { get; set; }
        public int Power { get; set; }
        //public int Attack => new Random().Next(0, Power);


        public int Attack
        {
            get
            {
                return new Random().Next(0, Power);
            }
            set
            {

            }
        }
        public bool IsDead { get; set; }
        //워리어 객체 생성시 외부에서 이름설정을 위해 string인자를 받을 생성자
        public Warrior(string name)
        {
            Name = name;
            HP = 100;
            Power = 20;
            IsDead = false;
        }
        public void TakeDamage(int damage)
        {
            Console.WriteLine($"{damage}만큼의 데미지를 입었다");
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                IsDead = true;
            }
        }

    }

    class Monster : ICharacter
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Power { get; set; }

        public int Attack => new Random().Next(0, Power);
        //public int Attack
        //{
        //    get
        //    {
        //        return new Random().Next(0, Power);
        //    }
        //    set
        //    {

        //    }
        //}
        public bool IsDead { get; set; }

        public Monster(string name, int hp, int power)
        {
            Name = name;
            HP = hp;
            Power = power;
            IsDead = false;
        }
        //Monster.TakeDamage(Player.Attack);
        //        Console.WriteLine($"{Player.Name}의 공격으로 {Monster.Name}은 {Player.Attack}만큼의 데미지를 입었다");
        //        Console.WriteLine($"{Monster.Name}의 HP는 {Monster.HP}만큼 남았다");
        public void TakeDamage(int damage)
        {
            Console.WriteLine($"{damage}만큼의 데미지를 입었다");
            HP -= damage;
            if (HP <= 0)
            {
                HP = 0;
                IsDead = true;
            }
        }

    }

    class Goblin : Monster
    {
        //실질적인 필드 값 초기화는 부모클래스에서 수행
        //부모클래스가 매개변수가 없는 생성자() 를 제공하지 않으면
        //파생클래스는 base를 사용해 부모생성자를 호출해야함
        //파생클래스 생성자가 받은 인자를 부모생성자에 넘길수있음
        public Goblin(string name)
        : base(name, 15, 10) { }
        //
    }

    class Orc : Monster
    {
        public Orc(string name)
        : base(name, 50, 30) { }
    }

    interface IItem
    {
        public string Name { get; }
        public void UseItem(ICharacter player)
        {

        }
    }

    class EmptyPotion : IItem
    {
        public string Name { get; }
        public EmptyPotion()
        {
            Name = "빈 병";
        }
        public void UseItem(ICharacter player)
        {
            Console.WriteLine($"빈 병이다.");
        }
    }

    class HPPotion : IItem
    {
        public string Name { get; }

        public HPPotion()
        {
            Name = "회복포션";
        }

        public void UseItem(ICharacter player)
        {
            Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            Console.WriteLine($"{player.Name}(은)는 {this.Name}을 벌컥벌컥마셨다");
            Console.Write($"{player.Name}의 HP는{player.HP}>");
            player.HP += 25;
            if (player.HP >= 100) player.HP = 100;
            Console.WriteLine($"{player.HP}로 변경되었다");
        }
    }

    class PowerPotion : IItem
    {
        public string Name { get; }

        public PowerPotion()
        {
            Name = "벌크업포션";
        }

        public void UseItem(ICharacter player)
        {
            Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            Console.WriteLine($"{player.Name}(은)는 {this.Name}을 벌크업벌크업마셨다");
            Console.Write($"{player.Name}의 힘은{player.Power}>");
            player.Power += 5;
            Console.WriteLine($"{player.Power}로 변경되었다");
        }
    }

    class Stage
    {
        private ICharacter Player { get;}
        private Monster Monster { get; }
        private List<IItem> DropTable { get; }

        public Stage(ICharacter player, Monster monster, List<IItem> dropTable)
        {
            Player = player;
            Monster = monster;
            DropTable = dropTable;
            Battle();
        }

        private void Battle()
        {
            Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            //플레이어가 죽을 때 까지 몬스터가 죽으면 내부에서 break;
            while (!Player.IsDead)
            {
                //캐릭터 > 몬스터.takedamage(attack)
                Console.Write($"{Player.Name}의 공격으로 {Monster.Name}(은)는 ");
                Monster.TakeDamage(Player.Attack);
                //Console.WriteLine($"{Player.Attack}만큼의 데미지를 입었다");
                Console.WriteLine($"{Monster.Name}의 HP는 {Monster.HP}만큼 남았다");
                Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
                //Thread.Sleep(1000);

                //몹 죽었냐?
                if (Monster.IsDead) break;

                //몬스터 > 캐릭터.takedamage(attack)
                Console.Write($"{Monster.Name}의 공격으로 {Player.Name}(은)는 ");
                Player.TakeDamage(Monster.Attack);
                //Console.WriteLine($"{Monster.Attack}만큼의 데미지를 입었다");
                Console.WriteLine($"{Player.Name}의 HP는 {Player.HP}만큼 남았다");
                Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
                //Thread.Sleep(1000);
            }

            if (Monster.IsDead)
            {
                Console.WriteLine($"{Monster.Name}(은)는 쓰러졌다");
                Console.WriteLine($"{Player.Name}의 HP는{Player.HP}이 남아있다");
                Reward(DropTable);
            }
            else if (Player.IsDead)
            {
                Console.WriteLine($"{Player.Name}(은)는 쓰러졌다");
            }

        }
        private void Reward(List<IItem> DropTable)
        {
            string inputStr;
            int input = 0;
            int goalCount = Program.goalCount;
            bool IsEmpty = false;
            /*
            string input;
            IItem selectedItem;
            */

            for (int i = 0; i < DropTable.Count; i++)
            {
                if (DropTable[i].Name == "빈 병")
                {
                    //Console.WriteLine($"Debug : 빈 병임");
                }
                else
                {
                    //Console.WriteLine($"Debug : 빈 병 아님");
                    break;
                }
                if(i==2)
                {
                    DropTable.RemoveRange(0, DropTable.Count);
                    IsEmpty = true;
                }
            }

            //선정된 드랍테이블에 내용물이 빈 병뿐이 아니고 마지막 전투가 아니면
            if (!IsEmpty && goalCount != 1)
            {
                //올바른 값을 입력했을때만 break;
                while (true)
                {
                    Console.WriteLine("어째선지 이 중 하나만 사용할수있다");

                    //유저가 사용할 아이템이름을 그대로 입력하기에는 시간이 너무 걸린다
                    //배열을 나열해주고 idx번호를 입력받아 사용시키고싶다
                    //유저입장에서의 시작은 1이다
                    for (int i = 0; i < DropTable.Count; i++)
                    {
                        i++;
                        Console.Write($"{i}");
                        i--;
                        var item = DropTable[i];
                        Console.WriteLine($".{item.Name}");
                    }
                    //3개가 같고 빈 병이면 선택을 건너뛰고 싶다
                    //DropTable[i]를 다 돌리고 item.Name이 "빈 병"이 아니면 break;
                    //"빈 병"이면 속행 반복문 끝나면 선택건너뛰고 안내문
                    

                    inputStr = Console.ReadLine();
                    //입력받은 string을 int로 자료형변환을 시도해 bool로 반환받는다
                    bool isCanInt = int.TryParse(inputStr, out int result);

                    //입력받은 값이 int가 아니면
                    if (isCanInt == false)
                    {
                        Console.WriteLine("정수를 입력하세요");
                    }
                    else
                    {
                        input = int.Parse(inputStr);
                    }

                    if (input > 0 && input <= DropTable.Count)
                    {
                        input--;//유저입장에서의 시작은 1이다
                        DropTable[input].UseItem(Player);
                        DropTable.RemoveRange(0, DropTable.Count);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("님아?");
                    }
                    


                    /*
                    foreach (var item in DropTable)
                    {
                        Console.WriteLine($"{item.Name}");
                    }
                    input = Console.ReadLine();

                    //List.함수 중 하나다 MS에서 그냥 이렇게 쓰세요 하는..
                    //List에서 입력값과 완전 같으면 값을 대입
                    selectedItem = DropTable.Find(item => item.Name == input);
                    
                    //입력값과 같지않아 값을 대입하지않으면 null
                    if (selectedItem != null)
                    {
                        DropTable.RemoveRange(0, DropTable.Count);
                        selectedItem.UseItem(Player);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("님아?");
                    }
                    */
                    
                }
                Console.WriteLine("하나를 집어드니 나머지는 사라졌다");
                Console.WriteLine("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓");
            }
            //마지막 전투면
            else if(goalCount == 1)
            {
                Console.WriteLine("이것이 마지막 전투였다 전리품은 눈에 들어오지 않는다.");
            }
            else
            {
                Console.WriteLine("이 녀석은 가진것이 없다.");
            }
        }
    }




    internal class Program
    {
        // = 1 마지막 전투라면 전리품을 고르지 않을것 StageManager Reward(List<IItem> DropTable)
        public static int goalCount = 5;

        static void Main(string[] args)
        {
            //플레이어블 캐릭터는 한번만 객체를 생성하면 된다
            Warrior player = new Warrior("전사");
            //몬스터들은 매번 새로운 객체를 생성해야한다
            Goblin goblin;
            Orc orc;

            EmptyPotion emptyPotion = new EmptyPotion();
            HPPotion hpPotion = new HPPotion();
            PowerPotion powerPotion = new PowerPotion();
            IItem[] arr_potion;
            List<IItem> DropTable = new List<IItem>();

            Stage stage;

            Console.WriteLine("5번 승리하지 않으면 빠져나갈 수 없는 동굴");
            //플레이어가 죽거나 5번 승리
            while (!player.IsDead && goalCount != 0)
            {
                Console.WriteLine($"앞으로{goalCount}번 승리해야 한다");
                Console.WriteLine("1.작은 통로\n2.큰 통로");
                int input = int.Parse(Console.ReadLine());

                if (input == 1)
                {
                    //arr_potion = new IItem[] { emptyPotion, hpPotion, powerPotion };
                    //인텔리 추천
                    arr_potion = [ emptyPotion, emptyPotion, hpPotion];

                    //드랍테이블의 구성품을 랜덤하게 넣고싶다
                    for (int i = 0; i < 3; i++)
                    {
                        DropTable.Add(arr_potion[new Random().Next(0, 3)]);
                        Console.WriteLine($"Debug : idx{i}:{DropTable[i].Name}");
                    }

                    goblin = new Goblin("고블린");
                    Console.WriteLine($"작은 통로에서 {goblin.Name}(을)를 조우했다");
                    stage = new Stage(player, goblin, DropTable);
                    goalCount--;
                }
                else if (input == 2)
                {

                    arr_potion = [emptyPotion, hpPotion, powerPotion];

                    for (int i = 0; i < 3; i++)
                    {
                        DropTable.Add(arr_potion[new Random().Next(0, 3)]);
                        Console.WriteLine($"Debug : idx{i}:{DropTable[i].Name}");
                    }

                    orc = new Orc("오크");
                    Console.WriteLine($"큰 통로에서 {orc.Name}(을)를 조우했다");
                    stage = new Stage(player, orc, DropTable);
                    goalCount--;
                }
                else
                {
                    Console.WriteLine("잘못된 입력");
                }
            }

            if(!player.IsDead)
            {
                Console.WriteLine("던전을 빠져나왔다.");
            }
            else
            {
                Console.WriteLine($"탈출 까지는 {goalCount}회의 전투가 더 남아 있었다\n몬스터의 한 끼 식사가 되었다");
            }
        }
    }
}
