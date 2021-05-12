using System.Collections;
using System.Collections.Generic;

//box factory to inherit from
public interface BoxFactory
{
	public ItemBoxData CreateRandomBox();
	public ItemBoxData CreateBox1();
	public ItemBoxData CreateBox2();
	public ItemBoxData CreateBox3();
}
