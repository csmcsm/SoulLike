using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItemStruct : MonoBehaviour
{
    public interface Item
    {
        string getName();
        string getDescription1();
        string getDescription2();
        string getImageName();
        //0：EvadePoint，1：防御，2:IdlePoint(RunPoint) 3、4、5：AttackPoint（-1代表没有），6：攻击， 7：自我侵蚀
        int[] getPower();
        string getType();

    }

    public class PunishmentKnight : Item
    {
        string Item.getDescription1()
        {
            return "光辉时代里的阴影，舍弃荣誉的戒律骑士，其面容如野兽般狰狞。";
        }

        string Item.getDescription2()
        {
            return "传说这套铠甲设计之初是为了对付某一位骑士长。";
        }

        string Item.getImageName()
        {
            return "PunishmentKnight";
        }

        string Item.getName()
        {
            return "戒律骑士";
        }

        int[] Item.getPower()
        {
            return new int[8] { 1, 30,1,1,2,11,10,10 };
        }
        //rightHand,body,normal
        string Item.getType()
        {
            return "body";
        }
    }
    public class WhiteKnight : Item
    {
        string Item.getDescription1()
        {
            return "传说中第一位叛教者，据说他杀害了某一位伟大的教皇。";
        }

        string Item.getDescription2()
        {
            return "光辉年代里，出身自白王国的骑士长，正直谦逊，深受骑士们的信赖。";
        }

        string Item.getImageName()
        {
            return "WhiteKnight";
        }

        string Item.getName()
        {
            return "白骑士";
        }

        int[] Item.getPower()
        {
            return new int[8] { 1, 20, 1, 1, 2, 3, 10, 0 };
        }
        string Item.getType()
        {
            return "body";
        }
    }
    public class WhiteKnightSword : Item
    {
        string Item.getDescription1()
        {
            return "传说中白骑士的佩剑。";
        }

        string Item.getDescription2()
        {
            return "极度危险，具有强烈的侵蚀性，须谨慎使用。";
        }

        string Item.getImageName()
        {
            return "白骑士大剑";
        }

        string Item.getName()
        {
            return "白骑士大剑";
        }

        int[] Item.getPower()
        {
            return new int[8] {1, 10, 2, 4, 5, 6, 50, 25 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class WeakOne : Item
    {
        string Item.getDescription1()
        {
            return "来历不明的武器。";
        }

        string Item.getDescription2()
        {
            return "危险度较强，侵蚀性中等。";
        }

        string Item.getImageName()
        {
            return "脆弱扳机";
        }

        string Item.getName()
        {
            return "脆弱扳机";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 1, 2, 3, 30, 10 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class SwordRod : Item
    {
        string Item.getDescription1()
        {
            return "制式武器之一。";
        }

        string Item.getDescription2()
        {
            return "危险度较低的武器，不具备侵蚀性。";
        }

        string Item.getImageName()
        {
            return "带刃杖";
        }

        string Item.getName()
        {
            return "带刃杖";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 2, 3, -1, 15, 0 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class Knife : Item
    {
        string Item.getDescription1()
        {
            return "制式武器之一。";
        }

        string Item.getDescription2()
        {
            return "危险度较低的武器，不具备侵蚀性。";
        }

        string Item.getImageName()
        {
            return "短刀";
        }

        string Item.getName()
        {
            return "短刀";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 7, 7, 3, 15, 0 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class LawKnightRod : Item
    {
        string Item.getDescription1()
        {
            return "以法术闻名的骑士，手中的长枪一定蕴含着奥秘。";
        }

        string Item.getDescription2()
        {
            return "危险度较低的武器，不具备侵蚀性。";
        }

        string Item.getImageName()
        {
            return "法骑士骑枪";
        }

        string Item.getName()
        {
            return "法骑士骑枪";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 7, 7, 3, 30, 10 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class StrangeHammer : Item
    {
        string Item.getDescription1()
        {
            return "外形怪异的大锤，其真实身份是……";
        }

        string Item.getDescription2()
        {
            return "危险度极高的武器，最好不要使用。";
        }

        string Item.getImageName()
        {
            return "怪异大锤";
        }

        string Item.getName()
        {
            return "怪异大锤";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1,4,5,6,80,80 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class PunishmentKnightHammer : Item
    {
        string Item.getDescription1()
        {
            return "体型巨大的大锤，哪怕是戒律骑士内部也只有一小部分精英才会使用。";
        }

        string Item.getDescription2()
        {
            return "危险度极高的武器，侵蚀性极强，非强者不可使用。不知为何配备了放血的结构，或许设计者有一个荒谬的想法。";
        }

        string Item.getImageName()
        {
            return "戒律骑士大锤";
        }

        string Item.getName()
        {
            return "戒律骑士大锤";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 4, 5, 6, 60, 40 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class PunishmentKnightKiller : Item
    {
        string Item.getDescription1()
        {
            return "枪与斧的组合，可灵活应对各种敌人，理应是极其优秀的武器，" +
                "但在戒律骑士内部却被视为离经叛道的象征，或许是因为这种武器背离了戒律骑士最初使命吧。";
        }

        string Item.getDescription2()
        {
            return "危险度较高的武器，但侵蚀性却不强。";
        }

        string Item.getImageName()
        {
            return "戒律骑士枪斧";
        }

        string Item.getName()
        {
            return "戒律骑士枪斧";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 4, 5, 6, 40, 15 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class PunishmentKnightPike : Item
    {
        string Item.getDescription1()
        {
            return "锋锐的长枪，便于放血的结构设计。只需要一击就可以让敌人失去行动能力。";
        }

        string Item.getDescription2()
        {
            return "危险度较高的武器，但侵蚀性却不强。";
        }

        string Item.getImageName()
        {
            return "戒律骑士刺枪";
        }

        string Item.getName()
        {
            return "戒律骑士刺枪";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 7, 5, 6, 40, 15 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class HuntingClaw : Item
    {
        string Item.getDescription1()
        {
            return "大到不自然的狩猎爪，真的有人会使用它吗？";
        }

        string Item.getDescription2()
        {
            return "危险度较高的武器，但侵蚀性却不强。";
        }

        string Item.getImageName()
        {
            return "狩猎爪";
        }

        string Item.getName()
        {
            return "狩猎爪";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 7, 2, 3, 40, 20 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class SmallBallHammer : Item
    {
        string Item.getDescription1()
        {
            return "小型的圆形锤，上面附着有神圣的气息。";
        }

        string Item.getDescription2()
        {
            return "危险度极低的武器。";
        }

        string Item.getImageName()
        {
            return "小圆锤";
        }

        string Item.getName()
        {
            return "小圆锤";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 7, 2, 3, 5, -15 };
        }
        string Item.getType()
        {
            return "rightHand";
        }
    }
    public class LightMog : Item
    {
        string Item.getDescription1()
        {
            return "圣者遗留下来的微光。";
        }

        string Item.getDescription2()
        {
            return "或许对于理智的恢复有着微弱的帮助。";
        }

        string Item.getImageName()
        {
            return "蓄光环";
        }

        string Item.getName()
        {
            return "蓄光环";
        }

        int[] Item.getPower()
        {
            return new int[8] { 2, 10, 1, 2, 3, -1, 15, 0 };
        }
        string Item.getType()
        {
            return "soul";
        }
    }
}
