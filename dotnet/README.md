
# Conclusions

## `ToList()` vs `ToArray()` - using net8.0

> Q: When realizing an enumerable is it better use `ToList()` or `ToArray()`?

- For small **int** arrays (100 items) it seems it is considerably faster to use `ToList()` (20%)
- For bigger **int** arrays both allocation and execution time are the same
- For classes (regardless of the number of properties) there are no noticeable differences
- For structs (regardless of the number of properties) there are no noticeable differences

## `Dictionary` vs `Array O(N)` - using net8.0

> Q: When searching by prop for an item in a collection is it better use `O(N) search over an array` or use a `Dictionary (HashMap)`?

- For an `int` key the array wins until N = 30~40
- For an `Guid` key the array wins until N = 10~20
- For an `string` key the array wins until N = 30~40
- For an `case insensitive string` key the array wins until N = 30~40

