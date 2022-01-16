public static class ItemsCountHolder
{
    
    public static int appleSaved;
    public static bool needUIUpdate = false;

    public static int countKeysUp;
    public static bool needChageSpriteBlackKey = false;

    public static int totalScore;


    //apples
    public static void AppleIncrese()
    {
        appleSaved++;
        needUIUpdate = true;
    }
    public static void AppleDecreaseToStart()
    {
        appleSaved =0;
        needUIUpdate = true;
    }


    //keys
    public static void KeyIncrease()
    {
        countKeysUp++;
        needChageSpriteBlackKey = true;
    }
    public static void KeyDecreaseToStart()
    {
        countKeysUp = 0;
    }

    //score

}
