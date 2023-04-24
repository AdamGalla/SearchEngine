pipeline {
    agent any
    trigger {
        pollSCM("* * * * *")
    }
    stages{
        stage("Build") {
            steps {
                sh "docker compose build"
            }
        }
        stage("Test") {
            steps {
                sh "docker compose up test"
            }
        }
        stage("Deliver") {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    sh 'docker login -u $USERNAME -p $PASSWORD'
                    sh "docker compose push"
                }
            }
        }
    }
}
