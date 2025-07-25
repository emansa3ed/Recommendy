﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class MyMemoryCache
	{
		public MemoryCache Cache { get; } = new MemoryCache(
			new MemoryCacheOptions
			{
				SizeLimit = 4L * 1024 * 1024 * 1024 // Assuming the server is 16GB, we used 25% of it.
			});
	}
}
