using System.Collections.Generic;

public interface IBox<Contained, ValType>
{
	ValType MoneyValue { get; }
	List<Contained> Contents { get; }

	void AddToBox(Contained contained);
	void RemoveFromBox(Contained contained);
}