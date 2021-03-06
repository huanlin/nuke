﻿// Copyright Matthias Koch, Sebastian Karasek 2018.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;
using static Nuke.Core.EnvironmentInfo;

namespace Nuke.Core.BuildServers
{
    /// <summary>
    ///     Interface according to the <a href="https://docs.gitlab.com/ce/ci/variables/README.html">official website</a>.
    /// </summary>
    [BuildServer]
    public class GitLab
    {
        private static Lazy<GitLab> s_instance = new Lazy<GitLab>(() => new GitLab());

        public static GitLab Instance => NukeBuild.Instance?.Host == HostType.GitLab ? s_instance.Value : null;

        internal static bool IsRunningGitLab => Variable("GITLAB_CI") != null;

        internal GitLab()
        {
        }

        /// <summary>
        /// Mark that job is executed in CI environment.
        /// </summary>
        public bool Ci => Variable<bool>("CI");

        /// <summary>
        /// The branch or tag name for which project is built.
        /// </summary>
        public string CommitRefName => Variable("CI_COMMIT_REF_NAME");

        /// <summary>
        /// <c>$CI_COMMIT_REF_NAME</c> lowercased, shortened to 63 bytes, and with everything except <c>0-9</c> and <c>a-z</c> replaced with <c>-</c>. No leading / trailing <c>-</c>. Use in URLs, host names and domain names.
        /// </summary>
        public string CommitRefSlug => Variable("CI_COMMIT_REF_SLUG");

        /// <summary>
        /// The commit revision for which project is built.
        /// </summary>
        public string CommitSha => Variable("CI_COMMIT_SHA");

        /// <summary>
        /// The commit tag name. Present only when building tags.
        /// </summary>
        public string CommitTag => Variable("CI_COMMIT_TAG");

        /// <summary>
        /// The path to CI config file. Defaults to <c>.gitlab-ci.yml</c>.
        /// </summary>
        public string ConfigPath => EnsureVariable("CI_CONFIG_PATH");

        /// <summary>
        /// Whether <a href="https://docs.gitlab.com/ce/ci/variables/README.html#debug-tracing">debug tracing</a> is enabled.
        /// </summary>
        public bool DebugTrace => Variable<bool>("CI_DEBUG_TRACE");

        /// <summary>
        /// Marks that the job is executed in a disposable environment (something that is created only for this job and disposed of/destroyed after the execution - all executors except <c>shell</c> and <c>ssh</c>). If the environment is disposable, it is set to true, otherwise it is not defined at all.
        /// </summary>
        public bool DisposableEnvironment => Variable<bool>("CI_DISPOSABLE_ENVIRONMENT");

        /// <summary>
        /// The name of the environment for this job.
        /// </summary>
        public string EnvironmentName => Variable("CI_ENVIRONMENT_NAME");

        /// <summary>
        /// A simplified version of the environment name, suitable for inclusion in DNS, URLs, Kubernetes labels, etc.
        /// </summary>
        public string EnvironmentSlug => Variable("CI_ENVIRONMENT_SLUG");

        /// <summary>
        /// The URL of the environment for this job.
        /// </summary>
        public string EnvironmentUrl => Variable("CI_ENVIRONMENT_URL");

        /// <summary>
        /// The unique id of the current job that GitLab CI uses internally.
        /// </summary>
        public string JobId => Variable("CI_JOB_ID");

        /// <summary>
        /// The flag to indicate that job was manually started.
        /// </summary>
        public bool JobManual => Variable<bool>("CI_JOB_MANUAL");

        /// <summary>
        /// The name of the job as defined in <c>.gitlab-ci.yml</c>.
        /// </summary>
        public string JobName => Variable("CI_JOB_NAME");

        /// <summary>
        /// The name of the stage as defined in <c>.gitlab-ci.yml</c>.
        /// </summary>
        public string JobStage => Variable("CI_JOB_STAGE");

        /// <summary>
        /// Token used for authenticating with the GitLab Container Registry.
        /// </summary>
        public string JobToken => Variable("CI_JOB_TOKEN");

        /// <summary>
        /// The URL to clone the Git repository.
        /// </summary>
        public string RepositoryUrl => Variable("CI_REPOSITORY_URL");

        /// <summary>
        /// The description of the runner as saved in GitLab.
        /// </summary>
        public string RunnerDescription => Variable("CI_RUNNER_DESCRIPTION");

        /// <summary>
        /// The unique id of runner being used.
        /// </summary>
        public string RunnerId => Variable("CI_RUNNER_ID");

        /// <summary>
        /// The defined runner tags.
        /// </summary>
        public string RunnerTags => Variable("CI_RUNNER_TAGS");

        /// <summary>
        /// The unique id of the current pipeline that GitLab CI uses internally.
        /// </summary>
        public string PipelineId => Variable("CI_PIPELINE_ID");

        /// <summary>
        /// The flag to indicate that job was <a href="https://docs.gitlab.com/ce/ci/triggers/README.html">triggered</a>.
        /// </summary>
        public bool PipelineTriggered => Variable<bool>("CI_PIPELINE_TRIGGERED");

        /// <summary>
        /// The source for this pipeline, one of: push, web, trigger, schedule, api, external. Pipelines created before 9.5 will have unknown as source.
        /// </summary>
        public string PipelineSource => Variable("CI_PIPELINE_SOURCE");

        /// <summary>
        /// The full path where the repository is cloned and where the job is run.
        /// </summary>
        public string ProjectDir => Variable("CI_PROJECT_DIR");

        /// <summary>
        /// The unique id of the current project that GitLab CI uses internally.
        /// </summary>
        public string ProjectId => Variable("CI_PROJECT_ID");

        /// <summary>
        /// The project name that is currently being built (actually it is project folder name).
        /// </summary>
        public string ProjectName => Variable("CI_PROJECT_NAME");

        /// <summary>
        /// The project namespace (username or groupname) that is currently being built.
        /// </summary>
        public string ProjectNamespace => Variable("CI_PROJECT_NAMESPACE");

        /// <summary>
        /// The namespace with project name.
        /// </summary>
        public string ProjectPath => Variable("CI_PROJECT_PATH");

        /// <summary>
        /// <c>$CI_PROJECT_PATH</c> lowercased and with everything except <c>0-9</c> and <c>a-z</c> replaced with <c>-</c>. Use in URLs and domain names.
        /// </summary>
        public string ProjectPathSlug => Variable("CI_PROJECT_PATH_SLUG");

        /// <summary>
        /// The HTTP address to access project.
        /// </summary>
        public string ProjectUrl => Variable("CI_PROJECT_URL");

        /// <summary>
        /// The project visibility (internal, private, public).
        /// </summary>
        public GitLabProjectVisibility ProjectVisibility => Variable<GitLabProjectVisibility>("CI_PROJECT_VISIBILITY");

        /// <summary>
        /// If the Container Registry is enabled it returns the address of GitLab's Container Registry.
        /// </summary>
        public string Registry => Variable("CI_REGISTRY");

        /// <summary>
        /// If the Container Registry is enabled for the project it returns the address of the registry tied to the specific project.
        /// </summary>
        public string RegistryImage => Variable("CI_REGISTRY_IMAGE");

        /// <summary>
        /// The password to use to push containers to the GitLab Container Registry.
        /// </summary>
        public string RegistryPassword => Variable("CI_REGISTRY_PASSWORD");

        /// <summary>
        /// The username to use to push containers to the GitLab Container Registry.
        /// </summary>
        public string RegistryUser => Variable("CI_REGISTRY_USER");

        /// <summary>
        /// Mark that job is executed in CI environment.
        /// </summary>
        public bool Server => Variable<bool>("CI_SERVER");

        /// <summary>
        /// The name of CI server that is used to coordinate jobs.
        /// </summary>
        public string ServerName => Variable("CI_SERVER_NAME");

        /// <summary>
        /// GitLab revision that is used to schedule jobs.
        /// </summary>
        public string ServerRevision => Variable("CI_SERVER_REVISION");

        /// <summary>
        /// GitLab version that is used to schedule jobs.
        /// </summary>
        public string ServerVersion => Variable("CI_SERVER_VERSION");

        /// <summary>
        /// Marks that the job is executed in a shared environment (something that is persisted across CI invocations like <c>shell</c> or <c>ssh</c> executor). If the environment is shared, it is set to true, otherwise it is not defined at all.
        /// </summary>
        public bool SharedEnvironment => Variable<bool>("CI_SHARED_ENVIRONMENT");

        /// <summary>
        /// Number of attempts to download artifacts running a job.
        /// </summary>
        public int ArtifactDownloadAttempts => Variable<int>("ARTIFACT_DOWNLOAD_ATTEMPTS");

        /// <summary>
        /// Number of attempts to fetch sources running a job.
        /// </summary>
        public int GetSourcesAttempts => Variable<int>("GET_SOURCES_ATTEMPTS");

        /// <summary>
        /// Mark that job is executed in GitLab CI environment.
        /// </summary>
        public bool GitLabCi => Variable<bool>("GITLAB_CI");

        /// <summary>
        /// The id of the user who started the job.
        /// </summary>
        public string GitLabUserId => Variable("GITLAB_USER_ID");

        /// <summary>
        /// The email of the user who started the job.
        /// </summary>
        public string GitLabUserEmail => Variable("GITLAB_USER_EMAIL");

        /// <summary>
        /// The login username of the user who started the job.
        /// </summary>
        public string GitLabUserLogin => Variable("GITLAB_USER_LOGIN");

        /// <summary>
        /// The real name of the user who started the job.
        /// </summary>
        public string GitLabUserName => Variable("GITLAB_USER_NAME");

        /// <summary>
        /// Number of attempts to restore the cache running a job.
        /// </summary>
        public int RestoreCacheAttempts => Variable<int>("RESTORE_CACHE_ATTEMPTS");
    }
}
