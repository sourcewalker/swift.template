﻿using Models.DTO;
using $safeprojectname$.$safeprojectname$.InstantWin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace $safeprojectname$.$safeprojectname$.InstantWin.Allocator.Algorithms
{
    public class WeightedAlgorithm : IAllocator
    {
        public IList<(Guid Id, string Name)> Allocate(IList<Allocable> allocable, int instantWinNumber)
        {
            var allocableList = new List<(Guid, string)>();

            var random = new Random();

            int loopNumber = Math.Min(instantWinNumber, allocable.Sum(a => a.Number));

            for (int index = 0; index < loopNumber; index++)
            {
                allocable = allocable.Where(p => p.Number > 0)
                               .OrderByDescending(p => p.Number)
                               .ToList();

                var ranIndex = PonderedRandomIndex(random, allocable, instantWinNumber);

                if (allocable[ranIndex].Number > 0)
                {
                    allocableList.Add((allocable[ranIndex].Id, allocable[ranIndex].Name));
                    allocable[ranIndex].Number -= 1;
                }
            }

            return allocableList;
        }

        private int PonderedRandomIndex(Random random, IList<Allocable> allocables, int instantWinNumber)
        {
            var ponderedIndex = 0;

            for (int index = 0; index < allocables.Count; index++)
            {
                var ranIndex = random.Next(instantWinNumber);
                var result = ranIndex - allocables[index].Number;

                if (result <= 0)
                {
                    ponderedIndex = index;
                    break;
                }
            }

            return ponderedIndex;
        }
    }
}
