using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string IMGname;
    public bool active = false;
    public Player player;
    public float timeLast, time;
    public virtual void SkillStart() { }
    public virtual void SkillEnd(float timeLast) { }
    public virtual string IMG() { return ""; }
    public void setPlayer(Player p)
    {
        player = p;
    }

    public class BloodGiven : Skill
    {
        public BloodGiven(Player p)
        {
            player = p;
        }
        public override string IMG() { return "redCup"; }
        public override void SkillStart()
        {
            active = true;
            time = 5;
            player.printf("BloodGivenStart");
        }
        public override void SkillEnd(float timeLast)
        {
            this.timeLast += timeLast;
            if (this.timeLast < this.time)
            {
                return;
            }
            active = false;
            player.printf("BloodGivenEnd");
        }
    }
    public class Escape : Skill
    {
        public Escape(Player p)
        {
            player = p;
        }
        public override string IMG() { return "escape"; }
        public override void SkillStart()
        {
            time = 1.5f;
            player.printf("EscapeStart");
            player.printf("EscapeStart");
        }
        public override void SkillEnd(float timeLast)
        {
            this.timeLast += timeLast;
            if (this.timeLast < this.time)
            {
                return;
            }
            active = false;
            player.printf("EscapeEnd");
        }
    }
    public class LibraryUse : Skill
    {
        public LibraryUse(Player p)
        {
            player = p;
        }
        public override string IMG() { return "libraryUse"; }
        public override void SkillStart()
        {
            player.printf("LibraryUseStart");
        }
        public override void SkillEnd(float timeLast)
        {

        }
    }


}
