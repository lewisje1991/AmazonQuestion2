using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonQuestion2
{
    public class Cloudfront
    {
        public int TimeToPopulate(int rows, int columns, int[][] grid)
        {
            var time = 0;
            while(GetNumberOfUnpopulatedServers(rows, columns, grid) > 0)
            {
                if(time > 10)
                {
                    throw new Exception("Too many attempts aborting");
                }

                var populatedServers = GetPopulatedServers(rows, columns, grid);

                foreach (var popServer in populatedServers)
                {
                    var adjacentServers = GetAdjacentServers(popServer.Key, popServer.Value, rows, columns);
                    SendToAdjacentServers(adjacentServers, grid);
                  
                }

                Console.WriteLine(JsonConvert.SerializeObject(grid));

                time++;
            }

            return time;
        }

        private void SendToAdjacentServers(List<AdjacentServerCoordinate> adjacentServers, int[][] grid)
        {
            foreach (var adjacentServer in adjacentServers)
            {
                grid[adjacentServer.x][adjacentServer.y] = 1;
            }
        }

        private List<AdjacentServerCoordinate> GetAdjacentServers(int x, int y, int rows, int columns)
        {
            var adjacentServers = new List<AdjacentServerCoordinate>();

            adjacentServers.AddRange(GetValidCoordinates(x, y, "y", columns));
            adjacentServers.AddRange(GetValidCoordinates(x, y, "x", rows));


            return adjacentServers;
        }


        private List<AdjacentServerCoordinate> GetValidCoordinates(int x, int y, string targetProperty, int limit)
        {
            var adjacentCoordinates = new List<AdjacentServerCoordinate>();
            var limitIndex = limit - 1;

            var decrementProperty = targetProperty == "y" ? y - 1 : x - 1;
            if (decrementProperty >= 0 && decrementProperty <= limitIndex)
            {
                adjacentCoordinates.Add(new AdjacentServerCoordinate()
                {
                    x = targetProperty == "x" ? decrementProperty : x,
                    y = targetProperty == "y" ? decrementProperty : y
                });
            }

            var incrementProperty = targetProperty == "y" ? y + 1 : x + 1;
            if (incrementProperty >= 0 && incrementProperty <= limitIndex)
            {
                adjacentCoordinates.Add(new AdjacentServerCoordinate()
                {
                    x = targetProperty == "x" ? incrementProperty : x,
                    y = targetProperty == "y" ? incrementProperty : y
                });
            }

            return adjacentCoordinates;
        }

        private List<KeyValuePair<int, int>> GetPopulatedServers(int rows, int columns, int[][] grid)
        {
            var serverCoordinates = new List<KeyValuePair<int,int>>();

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    if (grid[r][c] == 1)
                    {
                        serverCoordinates.Add(new KeyValuePair<int, int>(r,c));
                    }
                }
            }

            return serverCoordinates;
        }

        private int GetNumberOfUnpopulatedServers(int rows, int columns, int[][] grid)
        {
            var i = 0;

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    if (grid[r][c] != 1)
                    {
                        i++;
                    }
                }
            }

            return i;
        }
    }

    public class AdjacentServerCoordinate
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
