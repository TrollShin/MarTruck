/*
 * NavMash AreaMask 모음 입니다
 * 1 은 0000 0001 이라 1번째 영역만 활동하고
 * 17은 0001 0001 이라 1번째와 5번쨰 영역에서 활동합니다
 */
public enum EAreaMask
{
    Walkable = 1, // 0000 0001
    Walkable_CrossWalk = 17, // 0001 0001
}
