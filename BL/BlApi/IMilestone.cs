namespace BlApi;
public interface IMilestone
{
    public BO.Milestone Read(int id); //Reads milestone entity by its ID
    public BO.Milestone Update(BO.Milestone milestone);//Updates milestone entity
}
