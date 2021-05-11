using System.Collections;
using System.Collections.Generic;

//box factory to inherit from
public interface BoxFactory
{
	public ItemBox CreateRandomBox();
	public ItemBox CreateBox1();
	public ItemBox CreateBox2();
	public ItemBox CreateBox3();
}
