#include <stdio.h>
#include <unistd.h>
#include <pthread.h>

void* sillyThreadFunc(void* arg);

int main(void)
{
    pthread_t tid;

    fprintf(stderr,"Starting the main thread\n");

    //Nap for 2 seconds
    sleep(2);

    // create a second thread
    pthread_create(&tid, NULL, sillyThreadFunc, NULL);

    // pretend to work for a bit, but instead nap for another 4 seconds
    sleep(4);

    fprintf(stderr,"Ending the main thread\n");
    
    return 0;
}

void* sillyThreadFunc(void* arg)
{
    fprintf(stderr,"Starting the Silly Function thread\n");

    fprintf(stderr,"Threads are awesome!!1!\n");
    
    // pretend to work for a bit - nap for 2 seconds
    sleep(2);

    fprintf(stderr,"Ending the Silly Function thread\n");
    
    return NULL;
}
