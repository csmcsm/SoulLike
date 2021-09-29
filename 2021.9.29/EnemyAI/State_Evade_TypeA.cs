using System.Collections.Generic;
public class State_Evade_TypeA : IState_Move
{
    Troll troll;
    public Dictionary<string, string> openTheAni { get; set; }
    public Dictionary<string, string> closeTheAni { get; set; }
    public string[] exitAniTag;
    string nowAnitag;
    int lastAniTagHash;
    int nowAnitagHash;
    string StateName;
    float[] aimAtVector3;
    public void Excute()
    {
        //
        run();
        //
        nowAnitagHash = troll.ani.GetCurrentAnimatorStateInfo(0).tagHash;
        if (nowAnitagHash == lastAniTagHash) return;
        troll.useSpecialEffect(0);
        nowAnitag = troll.getCurrentAniStateTag(0);
        lastAniTagHash = nowAnitagHash;
    }
    public void Enter()
    {

        aimAtVector3 = troll.escapeAimAt();
        //if (aimAtVector3 == null) { troll.evadeState("move"); }
        troll.ani.SetBool("ifEvade", true);
        troll.printState(StateName);
        nowAnitagHash = troll.ani.GetCurrentAnimatorStateInfo(0).tagHash;
        troll.useSpecialEffect(0);
        nowAnitagHash = lastAniTagHash;
        nowAnitag = troll.getCurrentAniStateTag(0);
    }
    public void Exit()
    {
        nowAnitag = "";
        troll.ani.SetBool("ifEvade", false);
    }
    public void SetTroll(Troll troll)
    {
        this.troll = troll;
        troll.afterScaredTag = exitAniTag[0];
    }
    public void run()
    {
        troll.escape(aimAtVector3);
        if (nowAnitag == exitAniTag[0])
        {
            troll.ani.SetBool("ifEvade", false);
            if (troll.angry())
            {
                troll.forceToChangeState("attack");
            }
            else
            {
                troll.forceToChangeState("move");
            }
        }
    }
    public void aimAt()
    {

    }
    public State_Evade_TypeA(Dictionary<string, string> openTheAni,
     Dictionary<string, string> closeTheAni,
     string[] exitAniTag)
    {
        StateName = "State_Evade_TypeA";
        this.openTheAni = openTheAni;
        this.closeTheAni = closeTheAni;
        this.exitAniTag = exitAniTag;
    }
}