public class ObjectData
{
    public bool isSolved;
    public bool isUnlocked;
    public bool isRecentlySolved;
    public EScenesIndex sceneIndex;

    public void SetObjectData(bool solved,bool unlocked,bool recentlySolved,EScenesIndex eSceneIndex)
    {
        isSolved = solved;
        isUnlocked = unlocked;
        isRecentlySolved = recentlySolved;
        sceneIndex = eSceneIndex;
    }
}
