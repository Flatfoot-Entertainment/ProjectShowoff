using System.Collections.Generic;

public interface IBoxData<Contained>
{
	float MoneyValue { get; }
	List<Contained> Contents { get; }

	void AddToBox(Contained contained);
	void RemoveFromBox(Contained contained);
}