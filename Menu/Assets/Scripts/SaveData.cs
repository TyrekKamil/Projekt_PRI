﻿
[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current

    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }

        set
        {
            if (value != null)
            {
                _current = value;
            }
        }

    }

    public PlayerData playerData = new PlayerData();

}
