using System.Collections;
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
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {0, 7}, {1, 3} },
                new Dictionary<int, int>() { {2, 3} },
                new Dictionary<int, int>() { {2, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 3} }
            }
        },
        {
            Difficult.Medium, new List<Dictionary<int, int>>()
            {
                new Dictionary<int, int>() { {0, 3} },
                new Dictionary<int, int>() { {0, 5} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 7} },
                new Dictionary<int, int>() { {0, 3}, {1, 3} },
                new Dictionary<int, int>() { {0, 3}, {1, 5} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {0, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 3} },
                new Dictionary<int, int>() { {2, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 3} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {2, 5}, {1, 5} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 3} },
                new Dictionary<int, int>() { {3, 3}, {1, 5} }
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
                new Dictionary<int, int>() { {3, 5} },
                new Dictionary<int, int>() { {3, 5}, {1, 5} }
            }
        }
    };
    public static List<float> coefs = new List<float>();
}
