using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DUnion.Models;

internal readonly struct Sequence<T> : IEnumerable<T>, IEquatable<Sequence<T>>
{
    private static readonly int _typeId = typeof(Sequence<T>).GetHashCode();
    private readonly T[]? _values;

    public T this[int index] => _values is null ? throw new IndexOutOfRangeException() : _values[index];

    public readonly int Length => _values?.Length ?? 0;

    public Sequence(IEnumerable<T>? values)
    {
        _values = values.ToArray();
    }

    public override bool Equals(object obj)
    {
        return obj is Sequence<T> seq && Equals(seq);
    }

    public bool Equals(Sequence<T> other)
    {
        if (_values is null or { Length: 0 })
            return other._values is null or { Length: 0 };
        if (other._values is null or { Length: 0 })
            return _values.Length is 0;

        if (_values.Length != other._values.Length)
            return false;

        for (var i = 0; i < _values.Length; i++)
        {
            if (!Equals(_values[i], other._values[i]))
                return false;
        }

        return true;
    }

    public readonly Enumerator GetEnumerator()
    {
        return new(this);
    }

    public override readonly int GetHashCode()
    {
        if (_values is null)
            return (_typeId, 0).GetHashCode();

        var hash = (_typeId, _values.Length).GetHashCode();
        foreach (var item in _values)
            hash = (hash, item).GetHashCode();

        return hash;
    }

    public int IndexOf(T value)
    {
        if (_values is null)
            return -1;
        return Array.IndexOf(_values, value);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public struct Enumerator : IEnumerator<T>
    {
        private readonly Sequence<T> _source;

        /// <summary>
        /// index + 1, to allow 0 to be the unmoved state
        /// </summary>
        private int _state;

        public readonly T Current => _source._values![_state - 1];

        readonly object? IEnumerator.Current => Current;

        public Enumerator(Sequence<T> source)
        {
            _source = source;
        }

        public readonly void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_source._values is null || _state == _source._values.Length)
                return false;

            _state++;
            return true;
        }

        public void Reset()
        {
            _state = 0;
        }
    }
}