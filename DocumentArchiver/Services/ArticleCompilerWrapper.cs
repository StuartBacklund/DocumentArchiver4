namespace DocumentArchiver.Services
{
    public class ArticleCompilerWrapper
    {
        private ArticleCompiler articleCompiler;

        public ArticleCompilerWrapper(ArticleCompiler articleCompiler)
        {
            this.articleCompiler = articleCompiler;
        }

        public int Run()
        {
            this.articleCompiler.Setup();
            this.articleCompiler.CompileProject();
            return 1;
        }
    }
}