# C# Readonly Keyword Notes

## What is readonly?
readonly is a C# keyword used to declare a field whose value can only be assigned in:
1. Its declaration, or
2. A constructor
   - After the object is constructed, the field cannot be modified.
   - Readonly is used to make fields immutable after initialization.

## Why does readonly exist?
	1. Prevent accidental modification of important data
	2. Enforce immutability
	3. Improve thread-safety
	4. Make object state predictable
	5. Use values that are only known at runtime (unlike const)

	- Example uses:
	1. Services injected via DI
	2. Value objects
	3. Configuration settings
	4. Timestamps
	5. Complex objects initialized once

## Common Mistakes
	1. Thinking readonly makes objects immutable: Readonly only makes the reference immutable.
	2. Using const for non-primitive values: const DateTime Today = DateTime.Now → not allowed.
	3. Forgetting that const is inlined in IL: Changing const in a shared library requires ALL referencing projects to recompile.

## Best Practices
	1. Use readonly for injected services
	2. Use readonly for value objects
	3. Use readonly to enforce immutability
	4. Prefer static readonly for runtime constants
	5. Avoid const except for primitives/constants (e.g., Pi, MaxItems, Version)

## Examples inside /Readonly folder
	1. BasicReadonlyUsage.cs
	2. ReadonlyConstructorAssignment.cs
	3. ReadonlyVsConstComparison.cs
	4. ReadonlyReferenceMutationDemo.cs
	5. ReadonlyStructValueObject.cs
	6. ReadonlyDependencyInjectionDemo.cs

## Summary
	1. readonly → set once at runtime
	2. Used for immutability and safety
	3. Assigned only in constructor or declaration
	4. Protects reference, not content
	5. Different from const (compile-time)
	6. Ideal for DI, value objects, configs
	7. Supports any type (class/struct)