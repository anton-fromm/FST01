//using System;
//using Autofac;
//using Autofac.Extras.CommonServiceLocator;
//using CommonServiceLocator;

//namespace FST.TournamentPlanner.Test
//{
//    public abstract class IocTestBase
//    {
//        protected IocTestBase()
//        {
//            this.Container = new AutoSubstitute(b => this.ConfigureContainer(b));

//            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(this.Container.Container));
//        }
//        /// <summary>
//        /// Gibt den AutoSubstitute Container zurück.
//        /// </summary>
//        public AutoSubstitute Container { get; private set; }

//        /// <summary>
//        /// Kann verwendet werden um den Container nach einem Test zurückzusetzen.
//        /// Sieht Beispieltest: "ResetContainerTests".
//        /// </summary>
//        /// <param name="containerBuilderConfiguration"></param>
//        public void ResetContainer(Action<ContainerBuilder> containerBuilderConfiguration)
//        {
//            this.Container = new AutoSubstitute(containerBuilderConfiguration);
//            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(this.Container.Container));

//        }

//        protected virtual ContainerBuilder ConfigureContainer(ContainerBuilder builder)
//        {
//            return builder;
//        }
//    }
//}
