using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI_Task
{
    class SeasonsTask
    {
        class Season
        {
            public string Name { get; set; }

            public Dictionary<string, double> Params = new Dictionary<string, double>()
            {
                { "влажность", 0 },
                { "осадки", 0 },
                { "продолжительностьДня", 0 },
                { "температура", 0 },
            };

            public double MinParam;


            public Season(string name)
            {
                Name = name;
            }

            public void SetParam(string paramName, double value)
            {
                Params[paramName] = value;
            }

            public void CalcMinParam()
            {
                MinParam = Params.Values.Min();
            }

            public override string ToString()
            {
                return Name + " " + MinParam;
            }
        }

        Dictionary<string, double> _paramsToCheck = new Dictionary<string, double>()
            {
               { "влажность", 15 },
               { "осадки", 80 },
               { "продолжительностьДня", 12 },
               { "температура", 16 },
            };

        public SeasonsTask()
        {
            Season winter = new Season("зима");
            Season spring = new Season("весна");
            Season summer = new Season("лето");
            Season autumn = new Season("осень");
            List<Season> _seasons = new List<Season>() { winter, spring, summer, autumn };

            FileInfo[] files = (new DirectoryInfo("лингвПерем")).GetFiles("*.txt");

            foreach (var file in files)
            {
                FuncsManager funcManager = new FuncsManager(file.FullName);
                string paramName = file.Name.Replace(".txt", "");

                List<double> valuesForEachSeason = funcManager.GetAnswer(_paramsToCheck[paramName]);

                winter.SetParam(paramName, valuesForEachSeason[0]);
                spring.SetParam(paramName, valuesForEachSeason[1]);
                summer.SetParam(paramName, valuesForEachSeason[2]);
                autumn.SetParam(paramName, valuesForEachSeason[3]);

                //     (new MainForm(file.Name, funcManager)).Show();
            }

            foreach (var season in _seasons)
            {
                Console.WriteLine(season);
                season.CalcMinParam();
            }

            FuncsManager seasonsFM = new FuncsManager("Seasons.txt");


            seasonsFM.CutFuncWith("зима", new Line(new Point(0, winter.MinParam), new Point(6, winter.MinParam)));
            seasonsFM.CutFuncWith("весна", new Line(new Point(6, winter.MinParam), new Point(12, winter.MinParam)));
            seasonsFM.CutFuncWith("лето", new Line(new Point(12, winter.MinParam), new Point(18, winter.MinParam)));
            seasonsFM.CutFuncWith("осень", new Line(new Point(18, winter.MinParam), new Point(24, winter.MinParam)));

            (new ChartForm("Сезоны", seasonsFM)).Show();
            Console.WriteLine(seasonsFM.GetUnionMaxAverage());

        }
    }
}