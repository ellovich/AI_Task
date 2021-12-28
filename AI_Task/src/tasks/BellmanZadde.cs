using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Task
{
    /*
    Беллман-Задде
    выделение нечетких целей и нечетких ограничений
    uc - ограничение
    ug - цель = min{\1u*g1(x), ..., \1u*gn(x), ...,v1u*c1(x), ...,v1u*c1(x)}
     * \ и v - весовые штуки

    max ud(x) = max min(ug(x), uc(x))
    D - решение, явл. подмножеством ограничений C
     * фи от D являетсяс подмножеством G, т.е. удовл. выполнению целей
     * 
     * 
    1) выделение нечетких целей и ограничений
    2) формирование матрицы экспертной информации на основе результатов вып. шага 1
    3) применение процедуры взятия минимума с использованием указанных моделей преобразования информации
    4) применение процедуры ...
    */

    public class BellmanZadde
    {
        List<PcConfig> _pcConfigs;
        FuncsManager _fm;

        public BellmanZadde()
        {
            var cpu1 = new PcComponent("CPU1", 3);

            _pcConfigs = new List<PcConfig>() {
                new PcConfig(new List<PcComponent>() {
                    new PcComponent("CPU", 
                        new List<Goal>() { new Goal("частота", 3), new Goal("кэш", 9) }, 
                        new List<Confine>() { new Confine("цена", 20000) }),
                    new PcComponent("GPU", 
                        new List<Goal>() {}, 
                        new List<Confine>() {}),
                    new PcComponent("RAM", 
                        new List<Goal>() {}, 
                        new List<Confine>() {}),
                    new PcComponent("корпус",
                        new List<Goal>() {}, 
                        new List<Confine>() {}),
                    new PcComponent("батарея", 
                        new List<Goal>() {}, 
                        new List<Confine>() {}),
                }),
                new PcConfig(new List<PcComponent>() {}),
                new PcConfig(new List<PcComponent>() {}),
                new PcConfig(new List<PcComponent>() {}),
                new PcConfig(new List<PcComponent>() {}),
                new PcConfig(new List<PcComponent>() {}),
            };


            var funcs = _pcConfigs.Select(c => c.GetFuncs()).ToList();

            List<Func> fff = new List<Func>();
            foreach (var f in funcs)
                foreach (var d in f)
                    fff.Add(d);

            _fm = new FuncsManager(fff.ToArray());

            ChartForm cf = new ChartForm("PC Configs", _fm);
            cf.Show();
        }
    }

    public class PcConfig
    {
        List<PcComponent> _conmponents;

        public PcConfig(List<PcComponent> conmponents)
        {
            _conmponents = conmponents;
        }

        public List<Func> GetFuncs()
        {
            List<Func> funcs = new List<Func>();
            foreach (var item in _conmponents)
	        	 funcs.Add(Func.CreateFromValues(item.Name, item.GetGoalsAndConfinesPoints()));

            return funcs;
        }
    }

    public class PcComponent
    {
        public enum ComponentType
        {
            CPU,
            GPU,
            RAM,
            BATTERY,
        }

        public string Name { get; set; }
        public ComponentType Type { get; set; }
        public double Value { get; set; }

        public List<Goal> Goals { get; set; }
        public List<Confine> Confines { get; set; }

        public PcComponent(string name, List<Goal> goals, List<Confine> confine)
        {
            Name = name;
            Goals = goals;
            Confines = confine;
        }

        public PcComponent(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public List<double> GetGoalsAndConfinesPoints()
        {
            List<double> vals = Goals.Select(g => g.Val).ToList();
            vals.AddRange(Confines.Select(c => c.Val).ToList());

            return vals;
        }
    }

    public class Goal
    {
        public string Name { get; set; }
        public double Val { get; set; }

        public Goal(string name, double val)
        {
            Name = name;
            Val = val;
        }
    }

    public class Confine
    {
        public string Name { get; set; }
        public double Val { get; set; }

        public Confine(string name, double val)
        {
            Name = name;
            Val = val;
        }
    }
}

// Имеется транспортное средство, которому нужно выбрать маршрут, состоящий из пунктов назначения. 
// Чтобы решить задачу выбора оптимального маршрута нужно указать цель - определенные пункты должны быть посещены в определенное время. 
// усложнение: не для всех пунктов назначения имеется ограничение по времени.
// ограничения: безопасность маршртура - вероятность наступления экстримальной ситуации
             // равномерность скорости хода
// нужно выбрать оптимальный маршрут по целям и ограничениям 

// сверху маршрут
// в целях - посетить все, 