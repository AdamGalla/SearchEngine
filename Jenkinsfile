pipeline {
    agent any
    triggers {
        pollSCM("* * * * *")
    }
    post {
        failure {
            withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker compose down"
                    bat "docker compose pull"
                    bat "set /a BUILD_NUMBER-=1 && docker-compose up --build -d"
                }
           
        }
    }
    stages {
        stage("Build") {
            steps {
                bat "docker compose build"
            }
        }
        stage("Test") {
            steps {
                bat "docker compose up testdataformatter"
            }
        }
        stage("Deliver") {
            steps {
                withCredentials([usernamePassword(credentialsId: 'DockerHub', usernameVariable: 'USERNAME', passwordVariable: 'PASSWORD')]) {
                    bat 'docker login -u %USERNAME% -p %PASSWORD%'
                    bat "docker compose push"
                }
            }
        }
        stage("Deploy") {
            steps {
                bat "docker compose up -d"
            }
        }
    }
}
