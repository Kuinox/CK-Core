using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CK.Core.Collection.Tests
{
    [TestFixture]
    public class BestKeeperTests
    {
        readonly Random _random = new Random();

        [Test]
        public void add_some_candidates()
        {
            const int HeapSize = 16;
            int[] randomValues = Enumerable.Range( 0, 1000 ).Select( _ => _random.Next() ).ToArray();
            BestKeeper<int> sut = new BestKeeper<int>( HeapSize, ( n1, n2 ) => n1 - n2 );

            for( int i = 0; i < randomValues.Length; i++ )
            {
                sut.Add( randomValues[ i ] );
                Assert.That( sut.Count, Is.EqualTo( Math.Min( i + 1, HeapSize ) ) );
            }

            IEnumerable<int> best = randomValues.OrderByDescending( x => x ).Take( HeapSize );
            Assert.That( sut, Is.EquivalentTo( best ) );
        }

        [Test]
        public void benchmark()
        {
            const int HeapSize = 5000;
            int[] randomValues = Enumerable.Range( 0, 100000 ).Select( _ => _random.Next() ).ToArray();
            BenchmarkResult r1 = MicroBenchmark.MeasureTime( () =>
            {
                BestKeeper<int> sut = new BestKeeper<int>( HeapSize, ( n1, n2 ) => n1 - n2 );
                for( int i = 0; i < randomValues.Length; i++ ) sut.Add( randomValues[ i ] );
            }, 100 );

            BenchmarkResult r2 = MicroBenchmark.MeasureTime( () =>
            {
                OrderedArrayBestKeeper<int> sut = new OrderedArrayBestKeeper<int>( HeapSize, ( n1, n2 ) => n1 - n2 );
                for( int i = 0; i < randomValues.Length; i++ ) sut.Add( randomValues[ i ] );
            }, 100 );

            r1.IsSignificantlyBetterThan( r2 ).Should().BeTrue(); 
        }
    }
}