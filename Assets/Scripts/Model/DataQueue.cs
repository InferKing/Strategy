using System.Collections.Generic;

public static class DataQueue
{
    // here you can edit waves depends on difficulty
    public static readonly Dictionary<Difficult, List<Dictionary<int, int>>> listUnits = new Dictionary<Difficult, List<Dictionary<int, int>>>()
    {
        {
            Difficult.Easy, new List<Dictionary<int,int>>()
            {
                new Dictionary<int, int>() { {0, 3} },
                new Dictionary<int, int>() { {0, 3} },
                new Dictionary<int, int>() { {0, 3} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {0, 7}, {1, 3} },
                new Dictionary<int, int>() { {2, 3} },
                new Dictionary<int, int>() { {2, 3}, {0, 3} },
                new Dictionary<int, int>() { {2, 5}, {0, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {0, 5}, {1, 2}},
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {2, 3}, {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {2, 3}, {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {2, 3}, {0, 3} },
                new Dictionary<int, int>() { {3, 3}, {2, 3}, {1, 3} },
                new Dictionary<int, int>() { {4, 2} },
                new Dictionary<int, int>() { {4, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 3}, {4, 1} },
                new Dictionary<int, int>() { {4, 3}, {2, 3}, {0, 3} },
                new Dictionary<int, int>() { {3, 3}, {2, 3}, {1, 3} },
                new Dictionary<int, int>() { {4, 3}, {2, 3}, {3, 1} },
                new Dictionary<int, int>() { {4, 5} },
                new Dictionary<int, int>() { {4, 5} },
                new Dictionary<int, int>() { {4, 3}, {5, 2} },
                new Dictionary<int, int>() { {4, 2}, {5, 3} },
                new Dictionary<int, int>() { {4, 3}, {5, 3} },
                new Dictionary<int, int>() { {3, 3}, {4, 3} },
                new Dictionary<int, int>() { {4, 3}, {1, 5} },
                new Dictionary<int, int>() { {4, 3}, {1, 5}, {3, 3} },
                new Dictionary<int, int>() { {4, 4}, {5, 3} },
                new Dictionary<int, int>() { {4, 4}, {5, 3} },
                new Dictionary<int, int>() { {4, 4}, {5, 3} },
                new Dictionary<int, int>() { {6, 3} },
                new Dictionary<int, int>() { {6, 3}, {5, 3} },
                new Dictionary<int, int>() { {6, 3}, {4, 3} },
                new Dictionary<int, int>() { {6, 3}, {4, 3} },
                new Dictionary<int, int>() { {6, 3}, {4, 3}, {5, 1} },
                new Dictionary<int, int>() { {6, 3}, {4, 3}, {5, 2} },
                new Dictionary<int, int>() { {7, 3}},
                new Dictionary<int, int>() { {7, 3}, {6, 3} },
                new Dictionary<int, int>() { {7, 3}, {6, 2} },
                new Dictionary<int, int>() { {7, 4}, {5, 3} }
            }
        },
        {
            Difficult.Medium, new List<Dictionary<int, int>>()
            {
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 3}, {1, 5} },
                new Dictionary<int, int>() { {0, 5}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 3} },
                new Dictionary<int, int>() { {2, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 5}, {0, 2} },
                new Dictionary<int, int>() { {2, 5}, {1, 5}, {0, 2} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 5} },
                new Dictionary<int, int>() { {3, 3}, {2, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {2, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 5}, {1, 3} },
                new Dictionary<int, int>() { {4, 3} },
                new Dictionary<int, int>() { {4, 3} },
                new Dictionary<int, int>() { {4, 3}, {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {4, 3}, {1, 5} },
                new Dictionary<int, int>() { {4, 5}, {1, 3} },
                new Dictionary<int, int>() { {4, 5}, {1, 3}, {0, 5} },
                new Dictionary<int, int>() { {3, 3}, {5, 2} },
                new Dictionary<int, int>() { {4, 3}, {5, 2} },
                new Dictionary<int, int>() { {3, 3}, {4, 3}, {5, 3} },
                new Dictionary<int, int>() { {4, 3}, {5, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {5, 3}, {4, 3} },
                new Dictionary<int, int>() { {4, 3}, {5, 3}, {3, 3} },
                new Dictionary<int, int>() { {4, 5}, {5, 3}, {1, 3} },
                new Dictionary<int, int>() { {6, 3} },
                new Dictionary<int, int>() { {6, 3}, {5, 3} },
                new Dictionary<int, int>() { {6, 4}, {4, 3} },
                new Dictionary<int, int>() { {6, 4}, {4, 3} },
                new Dictionary<int, int>() { {6, 4}, {4, 3}, {5, 2} },
                new Dictionary<int, int>() { {7, 3}},
                new Dictionary<int, int>() { {7, 5}, {6, 2} },
                new Dictionary<int, int>() { {7, 5}, {5, 3} }
            }
        },
        {
            Difficult.Hard, new List<Dictionary<int, int>>()
            {
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 7}, {1, 5} },
                new Dictionary<int, int>() { {2, 7}, {1, 5} },
                new Dictionary<int, int>() { {3, 5} },
                new Dictionary<int, int>() { {3, 5}, {1, 5} },
                new Dictionary<int, int>() { {3, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 3}, {1, 3}, {3, 3} },
                new Dictionary<int, int>() { {2, 3}, {1, 3}, {3, 3} },
                new Dictionary<int, int>() { {4, 4} },
                new Dictionary<int, int>() { {4, 3}, {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {4, 3}, {1, 5} },
                new Dictionary<int, int>() { {4, 3}, {5, 2} },
                new Dictionary<int, int>() { {4, 5}, {5, 4} },
                new Dictionary<int, int>() { {6, 3} },
                new Dictionary<int, int>() { {6, 4}, {5, 3} },
                new Dictionary<int, int>() { {6, 3}, {4, 4}, {5, 3} },
                new Dictionary<int, int>() { {7, 3}},
                new Dictionary<int, int>() { {7, 5}, {6, 2}, {5, 2} },
                new Dictionary<int, int>() { {7, 5}, {5, 5} }
            }
        }
    };
    // 0 index is damage, 1 is speed, 2 is radius, 3 is health, 4 is add repair per sec
    public static readonly Dictionary<Difficult, List<float>> cannonCoefs = new Dictionary<Difficult, List<float>>() 
    {
        {
            Difficult.Easy, new List<float>()
            {
                1, 1, 1, 1500, 0
            }
        },
        {
            Difficult.Medium, new List<float>()
            {
                2f, 1.3f, 1.4f, 2500, 5
            }
        },
        {
            Difficult.Hard, new List<float>()
            {
                2.5f, 1.8f, 1.6f, 4000, 14
            }
        }
    };
    // 0 index is melee health, 1 is melee attack, 2 is area health, 3 is area damage
    public static readonly Dictionary<Difficult, List<float>> unitsCoefs = new Dictionary<Difficult, List<float>>()
    {
        {
            Difficult.Easy, new List<float>()
            {
                0.9f, 0.9f, 1, 0.8f
            }
        },
        {
            Difficult.Medium, new List<float>()
            {
                1.1f, 1.1f, 1, 1
            }
        },
        {
            Difficult.Hard, new List<float>()
            {
                1.2f, 1.2f, 1.2f, 1.1f  
            }
        }
    };
}
